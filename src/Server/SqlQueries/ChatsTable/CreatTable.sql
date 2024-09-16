CREATE TABLE chats(
	id uuid NOT NULL,
	firstuserid uuid NOT NULL,
	seconduserid uuid NOT NULL,
	createdat timestamp NOT NULL,
	updatedat timestamp,
	PRIMARY KEY (id),
	FOREIGN KEY (firstuserid) REFERENCES USERS(id),
	FOREIGN KEY (seconduserid) REFERENCES USERS(id)
);