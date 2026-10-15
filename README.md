# DownloadsWatcher

Um monitor de downloads ultraleve e nativo para Windows (C# .NET) que organiza automaticamente a pasta `Downloads` em subpastas categorizadas em tempo real.

## 🚀 Recursos
- **Execução Nativa**: Compilado como Windows GUI (`/target:winexe`), roda sem abrir janela preta de console (100% invisível).
- **Consumo Mínimo**: ~14 MB de RAM e 0.0% de CPU.
- **Detecção Inteligente**: Aguarda o navegador (Chrome, Edge, Firefox) finalizar a gravação (ignora extensões temporárias como `.crdownload` e `.tmp`).
- **Resolução de Conflitos**: Renomeia automaticamente arquivos com mesmo nome usando timestamp para evitar substituições acidentais.
- **Histórico**: Registra cada movimentação no arquivo `Downloads\_Historico_Organizacao.log`.

## 📂 Categorias Padrão
- **Instaladores**: `.exe`, `.msi`, `.bat`, `.cmd`, `.ps1`
- **Documentos**: `.pdf`, `.doc`, `.docx`, `.xls`, `.xlsx`, `.csv`, `.ppt`, `.pptx`, `.txt`, `.md`, `.epub`
- **Compactados**: `.zip`, `.rar`, `.7z`, `.tar`, `.gz`, `.iso`
- **Imagens**: `.png`, `.jpg`, `.jpeg`, `.gif`, `.webp`, `.svg`, `.bmp`, `.ico`, `.psd`
- **Vídeos**: `.mp4`, `.mkv`, `.avi`, `.mov`, `.wmv`, `.webm`
- **Áudios**: `.mp3`, `.wav`, `.flac`, `.ogg`, `.m4a`, `.aac`
- **Web & Código**: `.html`, `.json`, `.xml`, `.js`, `.css`, `.ts`, `.py`, `.rs`

## 🛠️ Como Compilar
Utilize o compilador nativo do .NET Framework já presente no Windows:
```cmd
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /target:winexe /optimize /out:DownloadsWatcher.exe DownloadsWatcher.cs
```

## ⚙️ Inicialização com o Windows
Para iniciar silenciosamente com o sistema, adicione uma entrada na chave de Registro:
- **Chave**: `HKCU\Software\Microsoft\Windows\CurrentVersion\Run`
- **Nome**: `DownloadsWatcher`
- **Valor**: `C:\caminho\para\DownloadsWatcher.exe`
