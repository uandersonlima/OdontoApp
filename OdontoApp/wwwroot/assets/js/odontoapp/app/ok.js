import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import React, { useEffect, useState } from "react";

export const Notify = () => {
  const [connection, setConnection] =
    (useState < null) | (HubConnection > null);
  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl("https://rokugan.fun/notificationhubservice")
      .withAutomaticReconnect()
      .build();

    setConnection(connect);
  }, []);

  useEffect(() => {
    const connectionOrders = async () => {
      if (connection) {
        connection
          .start()
          .then(() => {
            enableOrdersNotification();
            console.info("connected!");
          })
          .catch((err) => {
            if (connection.state === 0) {
              console.error(err.toString());
              setTimeout(connectionOrders, 3000);
            }
          });
      }
    };
    const enableOrdersNotification = async () => {
      connection.on("ReportNewPurchaseAsync", (order, title) => {
        console.log(order.id + "  " + title);
      });
    };

    connectionOrders();
  }, [connection]);
};
