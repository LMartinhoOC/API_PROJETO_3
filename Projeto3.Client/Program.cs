using Newtonsoft.Json;
using Projeto3.API.Models;
using Projeto3.Entidades;
using System.Net.Http.Headers;
using System.Text;

string token   = "";
string urlBase = "https://localhost:7005/api/";



//Obtem o token de autenticação
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    string json = JsonConvert.SerializeObject(
        new 
        { 
            login    = "admin",
            password = "admin" 
        });

    var body = new StringContent(json, Encoding.UTF8, "application/json");

    var resposta = await client.PostAsync("Auth/login", body);
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        TokenModel tokenModel = JsonConvert
            .DeserializeObject<TokenModel>(mensagem);

        token = tokenModel.token;
        Console.WriteLine($"Token adqurido. token: {token}\n");
    }
    else
    {
        Console.WriteLine($"Erro ao obter o token: {mensagem}");
    }
}



/*
//GET -> retorna toda a lista de clientes
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);

    var resposta = await client.GetAsync("Cliente");
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        List<Cliente> clientes = JsonConvert
            .DeserializeObject<List<Cliente>>(mensagem);

        Console.WriteLine("///GET///\nLISTA DE Clientes:");

        foreach (Cliente cliente in clientes)
        {
            Console.WriteLine(cliente.ToString());
            Console.WriteLine("");
        }
    }
    else
    {
        Console.WriteLine($"Erro ao realizar o GET: {mensagem}");
    }
}
*/


/*
//POST -> Insere um novo cliente no banco de dados
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);

    Cliente cliente    = new Cliente();
    cliente.NOME       = "Marco Antonio Pinheiro Marques";
    cliente.EMAIL      = "marco.pinheiro2@gmail.com";
    cliente.NASCIMENTO = new DateTime(1979, 07, 01);
    cliente.CPF        = "94715769034";
    cliente.ENDERECO   = "Rua teste 123";

    string json = JsonConvert.SerializeObject(cliente);

    var body = new StringContent(json, Encoding.UTF8, "application/json");

    var resposta = await client.PostAsync("Cliente", body);
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        Console.WriteLine($"Cliente {cliente.NOME} inserido com sucesso!");
    }
    else
    {
        Console.WriteLine($"Erro ao realizar o POST: {mensagem}");
    }
}
*/


/*
//PUT -> Atualiza um cliente no banco de dados
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);

    Cliente cliente    = new Cliente();
    cliente.ID         = 2002;
    cliente.NOME       = "Marco Antonio Pinheiro Marques";
    cliente.EMAIL      = "marco.pinheiro2@gmail.com";
    cliente.NASCIMENTO = new DateTime(1979, 07, 02);
    cliente.CPF        = "94715769034";
    cliente.ENDERECO   = "Rua Teste 1234 casa 10";

    string json = JsonConvert.SerializeObject(cliente);

    var body = new StringContent(json, Encoding.UTF8, "application/json");

    var resposta = await client.PutAsync("Cliente", body);
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        Console.WriteLine($"Cliente {cliente.NOME} atualizado com sucesso!");
    }
    else
    {
        Console.WriteLine($"Erro ao realizar o PUT: {mensagem}");
    }
}
*/



/*
//GET -> retorna um cliente pelo seu ID
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);

    var resposta = await client.GetAsync("Cliente/2002");
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        Cliente cliente = JsonConvert
            .DeserializeObject<Cliente>(mensagem);

        if (cliente != null)
        {
            Console.WriteLine(cliente.ToString());
        }                
    }
    else
    {
        Console.WriteLine($"Erro ao realizar o GET por ID: {mensagem}");
    }
}
*/

/*
//DELETE -> delete um cliente do banco de dados pelo seu ID
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(urlBase);

    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", token);

    var resposta = await client.DeleteAsync("Cliente/2002");
    var mensagem = resposta.Content.ReadAsStringAsync().Result;

    if (resposta.IsSuccessStatusCode)
    {
        Console.WriteLine("Cliente excluído com sucesso!");
    }
    else
    {
        Console.WriteLine($"Erro ao realizar o GET por ID: {mensagem}");
    }
}
*/



//Para a execução para ver o resultado
Console.WriteLine("Pressione qualquer tecla para continuar...");
Console.ReadKey();