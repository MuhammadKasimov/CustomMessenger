CREATE TABLE members(
	id uuid NOT NULL,
	userid uuid NOT NULL,
	groupid uuid NOT NULL,
	role integer NOT NULL,
	createdat timestamp NOT NULL,
	updatedat timestamp
	PRIMARY KEY(id),
	FOREIGN KEY (userid) REFERENCES users(id),
	FOREIGN KEY (groupid) REFERENCES groups(id) 
);