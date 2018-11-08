-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('GameCmd_pb')


local GAMECMD = protobuf.EnumDescriptor();
local GAMECMD_CMD_BASE_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_LOGIN_CMD_MIN_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_LOGIN_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_LOGIN_RESP_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_CHECK_NAME_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_CHECK_NAME_RESP_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_CREATE_PLAYER_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_CREATE_PLAYER_RESP_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_OFF_LINE_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_REGISTER_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_REGISTER_RESP_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_LOGIN_TEST_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_LOGIN_CMD_MAX_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_PLAYER_CMD_MIN_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_PLAYER_CMD_MAX_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ITEM_CMD_MIN_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ITEM_CMD_MAX_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_SOCIAL_CMD_MIN_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_SOCIAL_CMD_MAX_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_MISSION_CMD_MIN_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_MISSION_CMD_MAX_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_CREATE_ROOM_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_CREATE_ROOM_RESP_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_ADD_ROOM_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_ADD_ROOM_RESP_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_LEAVE_ROOM_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_READY_ROOM_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_READY_ROOM_RESP_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_CANCEL_ROOM_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_START_ROOM_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_REENTER_ROOM_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_START_BATTLE_RESP_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ASK_UPDATE_USER_DATA_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_UPDATE_USERS_DATA_RESP_ENUM = protobuf.EnumValueDescriptor();
local GAMECMD_ROOM_MAX_ENUM = protobuf.EnumValueDescriptor();

GAMECMD_CMD_BASE_ENUM.name = "CMD_BASE"
GAMECMD_CMD_BASE_ENUM.index = 0
GAMECMD_CMD_BASE_ENUM.number = 0
GAMECMD_LOGIN_CMD_MIN_ENUM.name = "LOGIN_CMD_MIN"
GAMECMD_LOGIN_CMD_MIN_ENUM.index = 1
GAMECMD_LOGIN_CMD_MIN_ENUM.number = 10000
GAMECMD_LOGIN_ENUM.name = "LOGIN"
GAMECMD_LOGIN_ENUM.index = 2
GAMECMD_LOGIN_ENUM.number = 10001
GAMECMD_LOGIN_RESP_ENUM.name = "LOGIN_RESP"
GAMECMD_LOGIN_RESP_ENUM.index = 3
GAMECMD_LOGIN_RESP_ENUM.number = 10002
GAMECMD_CHECK_NAME_ENUM.name = "CHECK_NAME"
GAMECMD_CHECK_NAME_ENUM.index = 4
GAMECMD_CHECK_NAME_ENUM.number = 10003
GAMECMD_CHECK_NAME_RESP_ENUM.name = "CHECK_NAME_RESP"
GAMECMD_CHECK_NAME_RESP_ENUM.index = 5
GAMECMD_CHECK_NAME_RESP_ENUM.number = 10004
GAMECMD_CREATE_PLAYER_ENUM.name = "CREATE_PLAYER"
GAMECMD_CREATE_PLAYER_ENUM.index = 6
GAMECMD_CREATE_PLAYER_ENUM.number = 10005
GAMECMD_CREATE_PLAYER_RESP_ENUM.name = "CREATE_PLAYER_RESP"
GAMECMD_CREATE_PLAYER_RESP_ENUM.index = 7
GAMECMD_CREATE_PLAYER_RESP_ENUM.number = 10006
GAMECMD_OFF_LINE_ENUM.name = "OFF_LINE"
GAMECMD_OFF_LINE_ENUM.index = 8
GAMECMD_OFF_LINE_ENUM.number = 10008
GAMECMD_REGISTER_ENUM.name = "REGISTER"
GAMECMD_REGISTER_ENUM.index = 9
GAMECMD_REGISTER_ENUM.number = 10010
GAMECMD_REGISTER_RESP_ENUM.name = "REGISTER_RESP"
GAMECMD_REGISTER_RESP_ENUM.index = 10
GAMECMD_REGISTER_RESP_ENUM.number = 10011
GAMECMD_LOGIN_TEST_ENUM.name = "LOGIN_TEST"
GAMECMD_LOGIN_TEST_ENUM.index = 11
GAMECMD_LOGIN_TEST_ENUM.number = 10017
GAMECMD_LOGIN_CMD_MAX_ENUM.name = "LOGIN_CMD_MAX"
GAMECMD_LOGIN_CMD_MAX_ENUM.index = 12
GAMECMD_LOGIN_CMD_MAX_ENUM.number = 10099
GAMECMD_PLAYER_CMD_MIN_ENUM.name = "PLAYER_CMD_MIN"
GAMECMD_PLAYER_CMD_MIN_ENUM.index = 13
GAMECMD_PLAYER_CMD_MIN_ENUM.number = 11100
GAMECMD_PLAYER_CMD_MAX_ENUM.name = "PLAYER_CMD_MAX"
GAMECMD_PLAYER_CMD_MAX_ENUM.index = 14
GAMECMD_PLAYER_CMD_MAX_ENUM.number = 11299
GAMECMD_ITEM_CMD_MIN_ENUM.name = "ITEM_CMD_MIN"
GAMECMD_ITEM_CMD_MIN_ENUM.index = 15
GAMECMD_ITEM_CMD_MIN_ENUM.number = 11300
GAMECMD_ITEM_CMD_MAX_ENUM.name = "ITEM_CMD_MAX"
GAMECMD_ITEM_CMD_MAX_ENUM.index = 16
GAMECMD_ITEM_CMD_MAX_ENUM.number = 11399
GAMECMD_SOCIAL_CMD_MIN_ENUM.name = "SOCIAL_CMD_MIN"
GAMECMD_SOCIAL_CMD_MIN_ENUM.index = 17
GAMECMD_SOCIAL_CMD_MIN_ENUM.number = 11900
GAMECMD_SOCIAL_CMD_MAX_ENUM.name = "SOCIAL_CMD_MAX"
GAMECMD_SOCIAL_CMD_MAX_ENUM.index = 18
GAMECMD_SOCIAL_CMD_MAX_ENUM.number = 11999
GAMECMD_MISSION_CMD_MIN_ENUM.name = "MISSION_CMD_MIN"
GAMECMD_MISSION_CMD_MIN_ENUM.index = 19
GAMECMD_MISSION_CMD_MIN_ENUM.number = 12800
GAMECMD_MISSION_CMD_MAX_ENUM.name = "MISSION_CMD_MAX"
GAMECMD_MISSION_CMD_MAX_ENUM.index = 20
GAMECMD_MISSION_CMD_MAX_ENUM.number = 12899
GAMECMD_ASK_CREATE_ROOM_ENUM.name = "ASK_CREATE_ROOM"
GAMECMD_ASK_CREATE_ROOM_ENUM.index = 21
GAMECMD_ASK_CREATE_ROOM_ENUM.number = 20000
GAMECMD_ASK_CREATE_ROOM_RESP_ENUM.name = "ASK_CREATE_ROOM_RESP"
GAMECMD_ASK_CREATE_ROOM_RESP_ENUM.index = 22
GAMECMD_ASK_CREATE_ROOM_RESP_ENUM.number = 20001
GAMECMD_ASK_ADD_ROOM_ENUM.name = "ASK_ADD_ROOM"
GAMECMD_ASK_ADD_ROOM_ENUM.index = 23
GAMECMD_ASK_ADD_ROOM_ENUM.number = 20002
GAMECMD_ASK_ADD_ROOM_RESP_ENUM.name = "ASK_ADD_ROOM_RESP"
GAMECMD_ASK_ADD_ROOM_RESP_ENUM.index = 24
GAMECMD_ASK_ADD_ROOM_RESP_ENUM.number = 20003
GAMECMD_ASK_LEAVE_ROOM_ENUM.name = "ASK_LEAVE_ROOM"
GAMECMD_ASK_LEAVE_ROOM_ENUM.index = 25
GAMECMD_ASK_LEAVE_ROOM_ENUM.number = 20004
GAMECMD_ASK_READY_ROOM_ENUM.name = "ASK_READY_ROOM"
GAMECMD_ASK_READY_ROOM_ENUM.index = 26
GAMECMD_ASK_READY_ROOM_ENUM.number = 20005
GAMECMD_ASK_READY_ROOM_RESP_ENUM.name = "ASK_READY_ROOM_RESP"
GAMECMD_ASK_READY_ROOM_RESP_ENUM.index = 27
GAMECMD_ASK_READY_ROOM_RESP_ENUM.number = 20006
GAMECMD_ASK_CANCEL_ROOM_ENUM.name = "ASK_CANCEL_ROOM"
GAMECMD_ASK_CANCEL_ROOM_ENUM.index = 28
GAMECMD_ASK_CANCEL_ROOM_ENUM.number = 20007
GAMECMD_ASK_START_ROOM_ENUM.name = "ASK_START_ROOM"
GAMECMD_ASK_START_ROOM_ENUM.index = 29
GAMECMD_ASK_START_ROOM_ENUM.number = 20008
GAMECMD_ASK_REENTER_ROOM_ENUM.name = "ASK_REENTER_ROOM"
GAMECMD_ASK_REENTER_ROOM_ENUM.index = 30
GAMECMD_ASK_REENTER_ROOM_ENUM.number = 20009
GAMECMD_START_BATTLE_RESP_ENUM.name = "START_BATTLE_RESP"
GAMECMD_START_BATTLE_RESP_ENUM.index = 31
GAMECMD_START_BATTLE_RESP_ENUM.number = 20010
GAMECMD_ASK_UPDATE_USER_DATA_ENUM.name = "ASK_UPDATE_USER_DATA"
GAMECMD_ASK_UPDATE_USER_DATA_ENUM.index = 32
GAMECMD_ASK_UPDATE_USER_DATA_ENUM.number = 20011
GAMECMD_UPDATE_USERS_DATA_RESP_ENUM.name = "UPDATE_USERS_DATA_RESP"
GAMECMD_UPDATE_USERS_DATA_RESP_ENUM.index = 33
GAMECMD_UPDATE_USERS_DATA_RESP_ENUM.number = 20012
GAMECMD_ROOM_MAX_ENUM.name = "ROOM_MAX"
GAMECMD_ROOM_MAX_ENUM.index = 34
GAMECMD_ROOM_MAX_ENUM.number = 20200
GAMECMD.name = "GameCmd"
GAMECMD.full_name = ".game_cmd.GameCmd"
GAMECMD.values = {GAMECMD_CMD_BASE_ENUM,GAMECMD_LOGIN_CMD_MIN_ENUM,GAMECMD_LOGIN_ENUM,GAMECMD_LOGIN_RESP_ENUM,GAMECMD_CHECK_NAME_ENUM,GAMECMD_CHECK_NAME_RESP_ENUM,GAMECMD_CREATE_PLAYER_ENUM,GAMECMD_CREATE_PLAYER_RESP_ENUM,GAMECMD_OFF_LINE_ENUM,GAMECMD_REGISTER_ENUM,GAMECMD_REGISTER_RESP_ENUM,GAMECMD_LOGIN_TEST_ENUM,GAMECMD_LOGIN_CMD_MAX_ENUM,GAMECMD_PLAYER_CMD_MIN_ENUM,GAMECMD_PLAYER_CMD_MAX_ENUM,GAMECMD_ITEM_CMD_MIN_ENUM,GAMECMD_ITEM_CMD_MAX_ENUM,GAMECMD_SOCIAL_CMD_MIN_ENUM,GAMECMD_SOCIAL_CMD_MAX_ENUM,GAMECMD_MISSION_CMD_MIN_ENUM,GAMECMD_MISSION_CMD_MAX_ENUM,GAMECMD_ASK_CREATE_ROOM_ENUM,GAMECMD_ASK_CREATE_ROOM_RESP_ENUM,GAMECMD_ASK_ADD_ROOM_ENUM,GAMECMD_ASK_ADD_ROOM_RESP_ENUM,GAMECMD_ASK_LEAVE_ROOM_ENUM,GAMECMD_ASK_READY_ROOM_ENUM,GAMECMD_ASK_READY_ROOM_RESP_ENUM,GAMECMD_ASK_CANCEL_ROOM_ENUM,GAMECMD_ASK_START_ROOM_ENUM,GAMECMD_ASK_REENTER_ROOM_ENUM,GAMECMD_START_BATTLE_RESP_ENUM,GAMECMD_ASK_UPDATE_USER_DATA_ENUM,GAMECMD_UPDATE_USERS_DATA_RESP_ENUM,GAMECMD_ROOM_MAX_ENUM}

ASK_ADD_ROOM = 20002
ASK_ADD_ROOM_RESP = 20003
ASK_CANCEL_ROOM = 20007
ASK_CREATE_ROOM = 20000
ASK_CREATE_ROOM_RESP = 20001
ASK_LEAVE_ROOM = 20004
ASK_READY_ROOM = 20005
ASK_READY_ROOM_RESP = 20006
ASK_REENTER_ROOM = 20009
ASK_START_ROOM = 20008
ASK_UPDATE_USER_DATA = 20011
CHECK_NAME = 10003
CHECK_NAME_RESP = 10004
CMD_BASE = 0
CREATE_PLAYER = 10005
CREATE_PLAYER_RESP = 10006
ITEM_CMD_MAX = 11399
ITEM_CMD_MIN = 11300
LOGIN = 10001
LOGIN_CMD_MAX = 10099
LOGIN_CMD_MIN = 10000
LOGIN_RESP = 10002
LOGIN_TEST = 10017
MISSION_CMD_MAX = 12899
MISSION_CMD_MIN = 12800
OFF_LINE = 10008
PLAYER_CMD_MAX = 11299
PLAYER_CMD_MIN = 11100
REGISTER = 10010
REGISTER_RESP = 10011
ROOM_MAX = 20200
SOCIAL_CMD_MAX = 11999
SOCIAL_CMD_MIN = 11900
START_BATTLE_RESP = 20010
UPDATE_USERS_DATA_RESP = 20012

