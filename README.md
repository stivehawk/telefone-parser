# Classe de parsemaento de telefone

[Link do source]https://github.com/stivehawk/telefone-parser/blob/master/TelefoneParser/Telefone.cs

```
var telefone = Telefone.Parse("99999-9999")
```

Exemplo de formatos válidos:
- 1234-5678
- 91111-1111
- (77) 91111-1111
- +55 (77) 91111-1111

Exemplo de parseamento:
```
var telefone = Telefone.Parse("+55 (11) 91111-1111")

var telefoneValido = telefone.TelefoneValido; // Código do estado e número são válidos

var numero = telefone.Numero; // 911111111
var numeroValido = telefone.NumeroValido; // True
var celular = telefone.NumeroCelular; // True

var codigoEstado = telefone.CodigoEstado; // 11
var codigoEstadoValido = telefone.CodigoEstadoValido; // True, encontrado entre os códigos de DDD do Brasil

var codigoPais = telefone.CodigoPais; // 55
```