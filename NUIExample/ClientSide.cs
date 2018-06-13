// NUIExample - Michael
// ClientSide.cs
// Last Cleanup: 11/06/2018 16:14
// Created: 11/06/2018 15:46

#region Directives
using System;
using System.Collections.Generic;
using System.Dynamic;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
#endregion

namespace NUIExample
{
    internal class ClientSide : BaseScript
    {
        public ClientSide()
        {
            // Register a command to trigger: Client -> NUI
            // use ingame: /sendnui
            API.RegisterCommand("sendnui", new Action<dynamic, List<dynamic>, string>(SendToNUI), false);

            // Register the callback, opens up http://exnui/helloworld (exnui = the name of the folder this resource is in)
            RegisterNUICallback("helloworld", ExecuteHelloWorld);
        }

        private void SendToNUI(object arg1, List<object> arg2, string arg3)
        {
            // create an ExpandoObject to hold our values
            // take note of the key value pairs made here, will be needed in javascript
            dynamic obj = new ExpandoObject();
            obj.action = "sendExample"; // both key and value can be whatever you like
            obj.examplemessage1 = "examplemessage1";
            obj.examplemessage2 = "examplemessage2";

            // Convert the ExpandoObject to a Json string.
            API.SendNuiMessage(JsonConvert.SerializeObject(obj));

            Debug.Write("Send NUI Message");
            API.SetNuiFocus(true, true);
        }

        private static CallbackDelegate ExecuteHelloWorld(dynamic arg)
        {
            Debug.Write("HelloWorld got executed!");
            Debug.Write($"We got this in our object: {arg.name} and {arg.lastname}");

            API.SetNuiFocus(false, false);

            return null;
        }

        private void RegisterNUICallback(string msg, Func<dynamic, CallbackDelegate> callback)
        {
            Debug.Write($"Registering NUI EventHandler for {msg}");
            API.RegisterNuiCallbackType(msg);

            EventHandlers[$"__cfx_nui:{msg}"] += new Action<dynamic>(body => { callback.Invoke(body); });
        }
    }
}