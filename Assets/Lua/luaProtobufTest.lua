local luaproto = CS.LoadProtoTxtTest
local pb = require "pb"
local protoc = require "Network/protoc"
local p=protoc.new()
local protoString

protoString=luaproto.LoadProtoBufFile("ectype.proto")
p:load(protoString,"ectype.proto")
print(pb.enum("ECtype",2))
print(pb.enum("ECtype","eUnameLoginReq"))

protoString=luaproto.LoadProtoBufFile("login3b.proto")
assert(p:load(protoString,"login3b.proto"))

protoString=luaproto.LoadProtoBufFile("login3a.proto")
assert(p:load(protoString,"login3a.proto"))

local data =
{
    name = 'ilse',
    age  = 18,
    contacts =
    {
        { name = 'alice', phonenumber = 12312341234 },
        { name = 'bob',   phonenumber = 45645674567 }
    }
}

local bytes = assert(pb.encode('Person', data))
print(pb.tohex(bytes))

local data2 = assert(pb.decode('Person', bytes))
print(data2.name)
print(data2.age)
print(data2.address)
print(data2.contacts[1].name)
print(data2.contacts[1].phonenumber)
print(data2.contacts[2].name)
print(data2.contacts[2].phonenumber)
