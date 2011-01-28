namespace java org.gatcdc.event.thrift
namespace cpp gat.event.thrift
namespace csharp Gat.Event.Thrift
namespace py GatEventThrift
namespace php Gat_Event_Thrift

enum GatEventType {
	INSERT,
	UPDATE,
	DELETE,
	CREATE,
	ALTER,
	DROP
}

enum GatDataType {
	ARRAY,
	BIGINT,
	BINARY,
	BIT,
	BLOB,
	BOOLEAN,
	CHAR,
	CLOB,
	DATALINK,
	DATE,
	DECIMAL,
	DISTINCT,
	DOUBLE,
	FLOAT,
	INTEGER,
	LONGNVARCHAR,
	LONGVARBINARY,
	LONGVARCHAR,
	NCHAR,
	NCLOB,
	NULL,
	NUMERIC,
	NVARCHAR,
	OTHER,
	REAL,
	REF,
	ROWID,
	SMALLINT,
	SQLXML,
	STRUCT,
	TIME,
	TIMESTAMP,
	TINYINT,
	VARBINARY,
	VARCHAR
}

struct GatColumnType {
	1: required GatDataType type,
	2: required string typeName,
	3: optional i32 precision,
	4: optional i32 scale
}

struct GatColumn {
	1: required i32 ordinal,
	2: required string name,
	3: required GatColumnType type
}

struct GatValue {
	1: required i32 ordinal,
	2: optional binary value
}

struct GatRow {
	1: required list<GatValue> values
}

struct GatRowSet {
	1: required list<GatColumn> columns,
	2: required list<GatRow> values
}

struct GatEvent {
	1: required string id,
	3: required GatEventType type,
	4: optional string catalog,
	5: optional string schema,
	6: optional string table,
	7: optional string sql,
	8: optional GatRowSet rowSet
}

struct GatEventResponse {
	1: required bool roolback = 0,
	2: optional string executeInstead,
	3: optional GatRowSet returnInstead,
	4: optional list<string> messages
}

exception GatRollbackException {
	1: optional string message
}

service GatConsumer {
	GatEventResponse onTrigger(1: GatEvent evt) throws(1: GatRollbackException err1)
}