CREATE TABLE order_side(
    id int PRIMARY KEY NOT NULL,
    side varchar(30) NOT NULL
);

CREATE TABLE order_type(
    id int PRIMARY KEY NOT NULL,
    type varchar(30) NOT NULL
);

CREATE TABLE order_status(
  id int PRIMARY KEY NOT NULL,
  status varchar(30) NOT NULL
);

CREATE TABLE account(
    id serial PRIMARY KEY NOT NULL,
    derivate varchar(30) NOT NULL,
    balance NUMERIC(10,4) NOT NULL,
    risk_per_trade NUMERIC(5,2) NOT NULL,
    currency varchar(25),
    exchange varchar(25)
);

CREATE TABLE trade(
  id serial PRIMARY KEY NOT NULL,
  risk_reward NUMERIC(5,2),
  late_entry int NOT NULL,
  candle_close_entry boolean,
  attempt int NOT NULL,
  percentage_parameters boolean,
  symbol varchar(25),
  stop_loss NUMERIC(10,4),
  take_profit NUMERIC(10,4),
  account_id int NOT null
);

CREATE TABLE trade_order(
	id serial PRIMARY KEY NOT NULL,
	quantity NUMERIC(10,4) NOT NULL,
	activation_price NUMERIC(10,4),
	filled_price NUMERIC(10,4),
	symbol varchar(25),
	fee NUMERIC(10,4),
	realized_profit  NUMERIC(10,4),
	created_date timestamp,
	closed_date timestamp,
	parent_order int,
	order_side int NOT null,
	order_type int NOT null,
	order_status int NOT null,
    trade_id int NOT null
);

CREATE TABLE trade_error(
  id serial PRIMARY KEY NOT NULL,
  created_date timestamp NOT NULL,
  message varchar(100),
  trade_id int NOT null
);

ALTER TABLE trade ADD CONSTRAINT fk_account_id FOREIGN KEY (account_id) REFERENCES account(id);
ALTER TABLE trade_order ADD CONSTRAINT fk_trade_id FOREIGN KEY (trade_id) REFERENCES trade(id);
ALTER TABLE trade_error ADD CONSTRAINT fk_trade_id FOREIGN KEY (trade_id) REFERENCES trade(id);
ALTER TABLE account ADD unique (id, derivate, exchange);




ALTER TABLE trade DROP CONSTRAINT fk_account_id;
ALTER TABLE trade_order DROP CONSTRAINT fk_trade_id;
ALTER TABLE trade_error DROP CONSTRAINT fk_trade_id;

drop table exchange;
drop table account;
drop table trade;
drop table trade_order;
drop table order_side;
drop table order_type;
drop table order_status;
drop table trade_error

SELECT *
FROM information_schema.columns
WHERE table_name = 'account';

select concat('alter table public.test1 drop constraint ', constraint_name) as my_query
from information_schema.table_constraints
where table_schema = 'public'
      and table_name = 'account'
      and constraint_type = 'PRIMARY KEY';


INSERT INTO order_side (id, name) VALUES (0, 'Buy');
INSERT INTO order_side (id,name) VALUES (1, 'Sell');

INSERT INTO order_type (id,name) VALUES (0, 'Limit');
INSERT INTO order_type (id,name) VALUES (1, 'Market');
INSERT INTO order_type (id,name) VALUES (2, 'Stop');
INSERT INTO order_type (id,name) VALUES (3, 'StopMarket');
INSERT INTO order_type (id,name) VALUES (4, 'TakeProfit');
INSERT INTO order_type (id,name) VALUES (5, 'TakeProfitMarket');
INSERT INTO order_type (id,name) VALUES (6, 'TrailingStopMarket');


INSERT INTO trade_mode (id,name) VALUES (0, 'Normal');
INSERT INTO trade_mode (id,name, risk_reward) VALUES (1, 'NormalSecure', 3);
INSERT INTO trade_mode (id,name, risk_reward) VALUES (2, 'Grid', 2);
INSERT INTO trade_mode (id,name, risk_reward) VALUES (3, 'GridSecure', 4);

INSERT INTO order_status (id,name) VALUES (0, 'New');
INSERT INTO order_status (id,name) VALUES (1, 'PartiallyFilled');
INSERT INTO order_status (id,name) VALUES (2, 'Filled');
INSERT INTO order_status (id,name) VALUES (3, 'Canceled');
INSERT INTO order_status (id,name) VALUES (4, 'PendingCancel');
INSERT INTO order_status (id,name) VALUES (5, 'Rejected');
INSERT INTO order_status (id,name) VALUES (6, 'Expired');

INSERT INTO account
(derivates, risk,  balance, currency)
VALUES('FUTURES', 1, 200, 'BUSD');

INSERT INTO account
(derivates, risk,  balance, currency)
VALUES('FUTURES', 1, 200, 'USDT');

INSERT INTO account
(derivates, risk,  balance, currency)
VALUES('COIN', 1, 0.05,  'BTC');

INSERT INTO account
(derivates, risk,  balance, currency)
VALUES('COIN', 1, 0.10,  'ETH');

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('BTCBUSD', now(), 1);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('ETHBUSD', now(), 1);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('BNBBUSD', now(), 1);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('ADABUSD', now(), 1);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('SOLBUSD', now(), 1);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('XRPBUSD', now(), 1);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('ADAUSDT', now(), 2);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('BTCBUSDT', now(), 2);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('ETHUSDT', now(), 2);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('BNBUSDT', now(), 2);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('SOLUSDT', now(), 2);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('BTCUSD_PERP', now(), 3);

INSERT INTO trade_system
(symbol, created_date , account_id)
VALUES('ETHUSD_PERP', now(), 4);