# Certifast Console

🚀 Aplicação em **C#/.NET** para automação do gerenciamento de certificados digitais.  
O sistema lê planilhas Excel com informações de certificados, gera alertas baseados em datas de vencimento e envia e-mails de notificação personalizados.  

---

## 📋 Visão Geral

O **Certifast Console** nasceu da necessidade de automatizar e tornar mais confiável o controle de certificados digitais, evitando riscos de expiração e perda de prazos.  

A aplicação foi desenvolvida com foco em:
- 🔎 **Monitoramento automático** de planilhas em uma pasta local  
- 📊 **Processamento contínuo** dos arquivos  
- ⏰ **Geração de alertas** conforme a proximidade do vencimento  
- 📧 **Envio de e-mails personalizados** para notificação  
- 🧩 Arquitetura desacoplada, utilizando **injeção de dependência** e testes unitários

---

## 🛠️ Tecnologias Utilizadas

- **C# / .NET 9**
- **Excel Interop** (leitura de planilhas)
- **SMTP** (envio de e-mails)
- **NUnit + Moq** (testes unitários e mocks)
- **Injeção de Dependência (DI)**
- **Clean Code & POO**

---

## ⚙️ Arquitetura

A arquitetura foi projetada para ser modular e testável:

- `Processor` → Orquestra o fluxo principal  
- `IExcelProcessor` → Interface para leitura de planilhas  
- `IEmailSender` → Interface para envio de notificações  
- `NoSqlDataBase` (mock inicial) → Serviço que armazena os alertas de cada certificado  
- **Mocks e testes unitários** garantem confiabilidade e manutenção futura 
