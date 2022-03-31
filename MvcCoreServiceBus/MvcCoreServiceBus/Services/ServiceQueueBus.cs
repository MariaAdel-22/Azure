using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MvcCoreServiceBus.Services
{
    public class ServiceQueueBus
    {
        private ServiceBusClient client;
        private List<string> mensajes;

        public ServiceQueueBus(string key)
        {
            this.client = new ServiceBusClient(key);
            this.mensajes = new List<string>();
        }

        //LOS PROCESOS DE RECEPCION DE MENSAJES SE REALIZAN DE FORMA ASINCRONA, SE UTILIZA UN METODO DELEGADO QUE IRA LEYENDO CADA
        //MENSAJE

        //METODO PARA ENVIAR MENSAJE
        public async Task SendMessageAsync(string data) {

            //PARA ENVIAR NECESITAMOS UN SENDER
            ServiceBusSender sender = this.client.CreateSender("developers");

            //EL OBJETO PARA ENVIAR MENSAJES ES MESSAGE
            ServiceBusMessage message = new ServiceBusMessage(data);

            await sender.SendMessageAsync(message);
        }

        //METODO PARA RECIBIR LOS MENSAJES. UTILIZA METODOS DELEGADOS PARA PROCESAR CADA LECTURA DE MENSAJE
        public async Task<List<string>> ReceiveMessageAsync() {

            ServiceBusProcessor processor = this.client.CreateProcessor("developers");

            //EL PROCESO DE LECTURA SE DEBE REALIZAR EN OTROS METODOS, ES DECIR, RELLENAR LA LISTA DE MENSAJES SE HACE
            //EN OTRO METODO. ESTE METODO LO QUE DEVUELVE SON LOS MENSAJES

            //DELEGAR METODO DE LECTURA
            processor.ProcessMessageAsync += Processor_ProcessMessageAsync;

            //DELEGAR METODO POR EXCEPCIONES
            processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

            //AQUI COMIENZA A LEER LA COLA DE MENSAJES
            await processor.StartProcessingAsync();

            Thread.Sleep(3000);

            //AQUI TERMINA LA LECTURA DE MENSAJES
            await processor.StopProcessingAsync();

            //DEVOLVEMOS LOS MENSAJES LEIDOS EN EL METODO Processor_ProccesMessageAsync
            return this.mensajes;
        }

        private async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            //AQUI LEEMOS CADA MENSAJE Y DECIDIMOS QUE HACER

            string content = arg.Message.Body.ToString();

            //AÑADIMOS LOS MENSAJES A NUESTRA CLASE LIST
            this.mensajes.Add(content);

            //DEBEMOS INDICAR QUE HEMOS PROCESADO ESTE MENSAJE
            await arg.CompleteMessageAsync(arg.Message);
        }

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            //EMTRA EM CASO DE QUE DIERA UNA EXCEPCION AL PROCESAR LOS MENSAJES

            Debug.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
