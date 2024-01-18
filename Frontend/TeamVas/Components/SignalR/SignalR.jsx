import { HubConnectionBuilder } from "@microsoft/signalr";

class SignalR {
  connection = null;

  startConnection = () => {
    this.connection = new HubConnectionBuilder()
      .withUrl("https://localhost:7232/messagehub")
      .withAutomaticReconnect()
      .build();

    this.connection.start().then(() => {
      console.log("SignalR connected");
    });
  };

  stopConnection = () => {
    this.connection.stop();
  };

  receiveMessage = (callback) => {
    this.connection.on("ReceiveMessage", (message) => {
      callback(message);
    });
  };

  sendMessage = (message) => {
    this.connection.invoke("SendMessage", message);
  };
}

const signalR = new SignalR();
export default signalR;