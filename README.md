# AppUseTime

このアプリケーションは、特定のソフトウエアの作業時間を集計する目的で作成されました。



<u>・Windows 10 環境</u>  

デフォルトはCLC GenomicsWorkbench です。

初回起動時に プログラムフォルダ内にあるCLC GenomicsWorkbench を検査します。

見つかれば、app.ini へフルパスを書き込みます。



もし mspaint を利用した人と時間を集計したい場合は、.exe フォルダ内に app.ini を作成し

その中に フルパスを記述します。



`app.ini`

`C`:\Windows\System32\mspaint.exe



起動時にName のみを入力するポップアップウインドウが表示され、acceptボタンを押下するとMSPatintが起動します。ポップアップウインドウは最小化されます。

MSPaint が終了されると、マイドキュメント内にapp-time.csv が生成されます。



