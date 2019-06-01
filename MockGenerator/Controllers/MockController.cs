using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HIAE.SIAF.Notificacoes.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MockGenerator.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockGenerator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MockController : ControllerBase
    {
        private readonly ICRUDService acrudService;

        public MockController(ICRUDService _acrudService)
        {
            acrudService = _acrudService;
        }
        // GET api/values
        [HttpGet("{api?}/{metodo?}")]
        public async Task<IActionResult> GetMock(
                                                         [FromHeader] string idSessao,
                                                         [FromHeader] string token,
                                                         string api,
                                                         string metodo)
        {
            var query = Request.QueryString.ToString();
            query = query.Remove(query.IndexOf("&hash"));

            string jString = System.IO.File.ReadAllText("MockGet.json");
            var json = JsonConvert.DeserializeObject<List<dynamic>>(jString);
            List<dynamic> MockResult = new List<dynamic>();

            foreach (var obj in json)
            {
                    
                    if (obj.rota.ToString() == $"{api}/{metodo}{query}")
                    {
                        MockResult.Add(obj);
                        
                    }
            }

            if (MockResult.Count > 0)
            {
                return new JsonResult(MockResult);
            }


            List<dynamic> result = await acrudService.Get(query, api, metodo, idSessao, token);

            foreach (var obj in result)
            {
                json.Add(obj);
            }
            string output = JsonConvert.SerializeObject(json, Formatting.Indented);
            System.IO.File.WriteAllText("MockGet.json", output);
            return new JsonResult(result);
        }




        // POST api/values
        [HttpPost("{api?}/{metodo?}")]
        public async Task<IActionResult> PostMock([FromBody] dynamic model, [FromHeader] string idSessao, [FromHeader] string token, string api, string metodo)
        {

            JObject modelAux = JsonConvert.DeserializeObject<JObject>(model.ToString());
            var propriedadesModel = modelAux.Properties().ToList();

            string jString = System.IO.File.ReadAllText("MockPost.json");
            var json = JsonConvert.DeserializeObject<List<dynamic>>(jString);

            foreach (var obj in json)
            {
                    if (obj.rota.ToString() == $"{api}/{metodo}")
                    {
                        JObject objAux = JsonConvert.DeserializeObject<JObject>(obj.Parametros.ToString());
                        var propriedadesObj = objAux.Properties().ToList();
                        var contador = 0;

                        foreach (var propertyModel in propriedadesModel)
                        {

                            foreach (var propertyObj in propriedadesObj)
                            {
                                if ((propertyModel.Name.ToString() == propertyObj.Name.ToString()) &&
                                    (propertyModel.Value.ToString() == propertyObj.Value.ToString()))
                                {
                                    contador++;
                                    if (contador == propriedadesModel.Count)
                                    {
                                        return new JsonResult(obj);
                                    }
                                }

                            }
                        }

                    }
            }

            ResultModel result = await acrudService.Post(model, api, metodo, idSessao, token);

            if (result.Resposta[0].CodigoMensagem == 120 || result.rota == "usuario/logar")
            {
                return new JsonResult(result);
            }

            result.Parametros = modelAux;
            json.Add(result);
            string output = JsonConvert.SerializeObject(json, Formatting.Indented);
            System.IO.File.WriteAllText("MockPost.json", output);
            return new JsonResult(result);

        }


        [HttpPost("{api?}/{metodo?}")]
        public async Task<IActionResult> PutMock([FromBody] dynamic model, [FromHeader] string idSessao, [FromHeader] string token, string api, string metodo)
        {

            JObject modelAux = JsonConvert.DeserializeObject<JObject>(model.ToString());
            var propriedadesModel = modelAux.Properties().ToList();

            string jString = System.IO.File.ReadAllText("MockPut.json");
            var json = JsonConvert.DeserializeObject<List<dynamic>>(jString);

            foreach (var obj in json)
            {
                if (obj.rota.ToString() == $"{api}/{metodo}")
                {
                    JObject objAux = JsonConvert.DeserializeObject<JObject>(obj.Parametros.ToString());
                    var propriedadesObj = objAux.Properties().ToList();
                    var contador = 0;

                    foreach (var propertyModel in propriedadesModel)
                    {

                        foreach (var propertyObj in propriedadesObj)
                        {
                            if ((propertyModel.Name.ToString() == propertyObj.Name.ToString()) &&
                                (propertyModel.Value.ToString() == propertyObj.Value.ToString()))
                            {
                                contador++;
                                if (contador == propriedadesModel.Count)
                                {
                                    return new JsonResult(obj);
                                }
                            }

                        }
                    }

                }
            }

            ResultModel result = await acrudService.Post(model, api, metodo, idSessao, token);

            if (result.Resposta[0].CodigoMensagem == 120 || result.rota == "usuario/logar")
            {
                return new JsonResult(result);
            }

            result.Parametros = modelAux;
            json.Add(result);
            string output = JsonConvert.SerializeObject(json, Formatting.Indented);
            System.IO.File.WriteAllText("MockPut.json", output);
            return new JsonResult(result);

        }

        // POST api/values
        [HttpPost("api/[controller]")]
        public JsonResult Inserir([FromBody] dynamic model)
        {

            JObject mock = new JObject();
            var _model = model;
            JObject modelAux = JsonConvert.DeserializeObject<JObject>(_model.Parametros.ToString());
            var proriedadesModel = modelAux.Properties().ToList();

            string jString = System.IO.File.ReadAllText("teste.json");
            var json = JsonConvert.DeserializeObject<List<dynamic>>(jString);
            int index = 0;
            foreach (var obj in json)
            {
                
                if (obj.rota.ToString() == model.rota.ToString())
                {
                    JObject objAux = JsonConvert.DeserializeObject<JObject>(obj.Parametros.ToString());
                    var proriedadesObj = objAux.Properties().ToList();
                    var contador = 0;

                    foreach (var propertyModel in proriedadesModel)
                    {

                        foreach (var propertyObj in proriedadesObj)
                        {
                            if ((propertyModel.Name.ToString() == propertyObj.Name.ToString()) &&
                                (propertyModel.Value.ToString() == propertyObj.Value.ToString()))
                            {
                                contador++;
                                if (contador == proriedadesModel.Count)
                                {
                                    json.RemoveAt(index);
                                    json.Add(model);
                                    string outputModel = JsonConvert.SerializeObject(json, Formatting.Indented);
                                    System.IO.File.WriteAllText("teste.json", outputModel);
                                    return new JsonResult(new { status = 1, msg = "Mock Atualizado!" });
                                }
                            }

                        }
                    }

                }
                index++;
            }

            json.Add(model);
            string output = JsonConvert.SerializeObject(json, Formatting.Indented);
            System.IO.File.WriteAllText("teste.json", output);
            return new JsonResult(new { status = 1, msg = "Mock cadastrado com sucesso!" });

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
