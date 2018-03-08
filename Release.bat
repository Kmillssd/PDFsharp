copy "src\Pdfsharp\bin\Release\PdfSharp.dll" "packaging\dist\PdfSharp.dll"
copy "src\Pdfsharp.Charting\bin\Release\PdfSharp.Charting.dll" "packaging\dist\PdfSharp.Charting.dll"

"packaging/bin/nuget.exe" pack "packaging/Kmillssd.Pdfsharp.nuspec" -outputdirectory "packaging/"

"packaging/bin/nuget.exe" push "packaging/Kmillssd.PdfSharp.1.51.1.nupkg" -ApiKey -Source https://www.nuget.org -verbosity normal