using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain.TextToSpeech.Interface;

namespace WakuLive.Core.Domain
{
    public class TextToSpeechByBouyomiChanService : ITextToSpeechByBouyomiChanService
    {
        private IDisposable _disposable;
        private bool _isReady;
        public TextToSpeechByBouyomiChanService() 
        {
            _disposable = Observable.Interval(TimeSpan.FromMilliseconds(5000))
                                    .StartWith(0)
                                    .SubscribeOn(Scheduler.ThreadPool)
                                    .Subscribe(x =>
                                    {
                                        _isReady = CheckBouyomiChanIsReady();
                                        Debug.Print("Bouyomichan is ready:" + IsReady());
                                    });
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void AddSpeech(string message)
        {
            if (!_isReady) { return; }

            TcpClient client = null;
            try
            {
                client = CreateTcpClient();
            }
            catch (Exception ex)
            {
                return;
            }

            SendData(client, message);

            client.Close();
        }

        public bool IsReady()
        {
            return _isReady;
        }

        private bool CheckBouyomiChanIsReady() 
        {
            bool isReady = true;

            TcpClient client = null;
            try
            {
                client = CreateTcpClient();
            }
            catch (Exception ex)
            {
                isReady = false;
            }

            if (client != null)
            {
                SendData(client, "");
            }

            return isReady;
        }

        private TcpClient CreateTcpClient() 
        {
            string sHost = "127.0.0.1"; //棒読みちゃんが動いているホスト
            int iPort = 50001;       //棒読みちゃんのTCPサーバのポート番号(デフォルト値)
            TcpClient client = new TcpClient(sHost, iPort);
            return client;
        }

        private void SendData(TcpClient client, string message) 
        {
            byte bCode = 0;          //文字列のbyte配列の文字コード(0:UTF-8, 1:Unicode, 2:Shift-JIS)
            Int16 iVoice = 0;        //声質    （ 0:棒読みちゃん画面上の設定、1:女性1、2:女性2、3:男性1、4:男性2、5:中性、6:ロボット、7:機械1、8:機械2、10001～:SAPI5）
            Int16 iVolume = -1;      //音量    （-1:棒読みちゃん画面上の設定）
            Int16 iSpeed = -1;       //速度    （-1:棒読みちゃん画面上の設定）
            Int16 iTone = -1;        //音程    （-1:棒読みちゃん画面上の設定）
            Int16 iCommand = 0x0001; //コマンド（ 0:メッセージ読み上げ）
            byte[] bMessage = Encoding.UTF8.GetBytes(message);
            Int32 iLength = bMessage.Length;

            using (NetworkStream ns = client.GetStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ns))
                {
                    bw.Write(iCommand);
                    bw.Write(iSpeed);
                    bw.Write(iTone);
                    bw.Write(iVolume);
                    bw.Write(iVoice);
                    bw.Write(bCode);
                    bw.Write(iLength);
                    bw.Write(bMessage);
                }
            }
        }
    }
}
