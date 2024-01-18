import { useRef, useEffect, useState } from "react";
import signalR from "../SignalR/SignalR";
import styles from './Chat.module.css';
import keycloak from '../../src/Utils/useAuth.jsx';


function Chat({  }) {
  

  const [scrollButton, setScrollButton] = useState(false);
  const [scrollToBottomValue, setScrollToBottom] = useState(false);
  const messageInputRef = useRef();
  const messageListRef = useRef();
  const [messages, setMessages] = useState([]);
  const [filteredMessages, setFilteredMessages] = useState([]);


  useEffect(() => {
    if (scrollToBottomValue) {
      setScrollToBottom(false);
      setTimeout(() => {
        scrollToBottom();
      }, 100);
    }
  }, [scrollToBottomValue]);

  useEffect(() => {
    signalR.startConnection();
  
    signalR.receiveMessage((message) => {
      setMessages((prevMessages) => [...prevMessages, message]);
      setScrollToBottom(true);
    });
  
    return () => {
        signalR.stopConnection();
    };
  }, []);

  function scrollToBottom() {
    if (messageListRef.current) {
      messageListRef.current.scrollTop = messageListRef.current.scrollHeight;
    }
  }

  const sendMessage = async () => {
    const messageContent = messageInputRef.current.value;
    if (messageContent !== "") {
      try {
        await signalR.sendMessage({
          UserSender: keycloak.tokenParsed?.preferred_username,
          Message: messageContent
        });
        messageInputRef.current.value = '';
        setScrollToBottom(true);
      } catch (error) {
        console.error('Sending message failed: ', error);
      }
    }
  };

  const handleKeyDown = (event) => {
    if (event.key === 'Enter') {
      event.preventDefault();
      sendMessage();
    }
  };

  return (
    <main>
        <div className={styles.chatContainer}>
        <h2 className="font-medium text-2xl mb-6">Chat</h2>
        <div className={styles.messagesList} ref={messageListRef}>
            {messages.map((msg, index) => (
            <ul key={index}>
                <li className={styles.messageItem}>
                <div className={`${styles.messageWrapper} ${msg.userSender === keycloak.tokenParsed?.preferred_username ? styles.myMessage : styles.otherMessage}`}>
                    <div className={styles.senderInfo}>
                    <p className={styles.senderName}>{msg.userSender}</p>
                    <div className={styles.divider}></div>
                    </div>
                    <p className={styles.messageText}>{msg.message}</p>
                </div>
                </li>
            </ul>
            ))}
        </div>
        <div className={styles.inputContainer}>
            <input
            ref={messageInputRef}
            className={styles.inputBox}
            placeholder="Type a message"
            onKeyDown={handleKeyDown}
            />
            <button
            onClick={sendMessage}
            className={styles.sendButton}
            >
            Send
            </button>
        </div>
        </div>
    </main>
  );
}

export default Chat;
