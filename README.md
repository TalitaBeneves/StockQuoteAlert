# StockQuoteAlert

## Visão Geral
O StockQuoteAlert é um sistema desenvolvido para enviar alertas por e-mail quando a cotação de um ativo da B3 cair abaixo ou subir acima de determinados níveis.

## Parâmetros

O sistema deve ser executado via linha de comando com três parâmetros:

1- Ativo a ser monitorado: o código do ativo (por exemplo, PETR4) <br/>
2- Preço de referência para venda: o preço acima do qual o alerta de venda será disparado <br/>
3- Preço de referência para compra: o preço abaixo do qual o alerta de compra será disparado <br/>

## Uso
As configurações do servidor SMTP para envio de e-mails estão no arquivo Config.json, que deve ser editado com as informações corretas.

Para executar o sistema, navegue até o diretório bin/Debug/net8.0/ e execute o seguinte comando: 
> dotnet StockQuoteAlert.dll PETR4 22.67 22.59

