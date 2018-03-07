copy "src\Pdfsharp\bin\Release\PdfSharp.dll" "packaging\dist\PdfSharp.dll"
copy "src\Pdfsharp.Charting\bin\Release\PdfSharp.Charting.dll" "packaging\dist\PdfSharp.Charting.dll"

"packaging/bin/nuget.exe" pack "packaging/Kmillssd.Pdfsharp.nuspec" -outputdirectory "packaging/"