CREATE TABLE public.messages (
	id uuid NOT NULL,
	"content" varchar NOT NULL,
	senderid uuid NOT NULL,
	chatid uuid NULL,
	groupid uuid NULL,
	createdat timestamptz NOT NULL,
	updatedat timestamptz NULL,
	CONSTRAINT messages_pkey PRIMARY KEY (id),
	CONSTRAINT messages_chatid_fkey FOREIGN KEY (chatid) REFERENCES public.chats(id),
	CONSTRAINT messages_groupid_fkey FOREIGN KEY (groupid) REFERENCES public."groups"(id),
	CONSTRAINT messages_senderid_fkey FOREIGN KEY (senderid) REFERENCES public.users(id)
);