## WakuLive
WakuLiveはTwitch配信のコメント読み上げビューワーソフトです。
URL入力で配信を開き、棒読みちゃんとの連携で読み上げを行います（別途棒読みちゃんの起動が必要）

## ダウンロード
https://drive.google.com/file/d/1f3Kc55m6Ub5a6T1HXRlw4NV58e52wmfQ/view?usp=drive_link

## 使い方
アプリの使い方の実演解説（3分ほどの動画です）
https://www.youtube.com/watch?v=t6g_N1ZmVAw

コメント取得を行うにはまず認証が必要です。
1. 設定→Twitchの認証ボタンを押します。
2. ブラウザ上でTwitchのOAuth認証画面が開かれるので、「許可」を押して認証してください（Twitchアカウントでのログインが必要です）
3. 認証ボタンが「認証済」になったら認証完了です。URL入力でTwitch配信を開きコメント取得を開始します。
![WakuLive](https://github.com/mojopon/WakuLive/assets/2043596/7a2968f7-3b4d-40de-8000-709db29e7201)
チャット読み上げのチェックをONにすることで、棒読みちゃんと連携してコメントの読み上げを開始します。
※あらかじめ棒読みちゃん( https://chi.usamimi.info/Program/Application/BouyomiChan/ )をダウンロードし、起動する必要があります

## ビルド方法
1. WakuLiveプロジェクト（ https://github.com/mojopon/WakuLive.git ）をClone
2. Visual Studioでプロジェクトファイルを開く
3. ProjectのConfigurationをReleaseにしてビルド（Debugだとオフラインでのデバッグ環境のビルドとなります）

## 基本設計
アプリケーションの設計についてはこちらの記事で解説しています。
https://qiita.com/mojomojopon/private/27b16cc17538549f25c5
