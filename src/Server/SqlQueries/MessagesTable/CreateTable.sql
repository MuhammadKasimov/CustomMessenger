CREATE TABLE messages(
	id uuid NOT NULL,
	content varchar NOT NULL,
	senderid uuid NOT NULL,
	chatid uuid,
	groupid uuid,
	createdat timestamp NOT NULL,
	updatedat timestamp,
	PRIMARY KEY(id),
	FOREIGN KEY (senderid) REFERENCES users(id),
	FOREIGN KEY (chatid) REFERENCES chats(id),
	FOREIGN KEY (groupid) REFERENCES groups(id)
);


