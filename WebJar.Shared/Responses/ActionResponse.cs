﻿namespace WebJar.Shared.Responses
{
    public class ActionResponse<T>
    {
        //Nos indica si la acion fue exitosa o no
        public bool WasSuccess { get; set; }

        //Nos da el mensaje de error
        public string? Message { get; set; }

        //Es el resultado de la accion
        public T? Result { get; set; }

        //Si la operacion fue exitosa devuelve Result si no lo es devuelve Message
    }
}