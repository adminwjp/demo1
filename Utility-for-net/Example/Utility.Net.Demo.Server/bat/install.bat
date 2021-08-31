%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe WindowsService.exe
Net Start CrawlService
sc config CrawlService start= auto