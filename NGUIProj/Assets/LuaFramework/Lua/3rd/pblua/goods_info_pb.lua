-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('goods_info_pb')


local GOODS_INFO = protobuf.Descriptor();
local GOODS_INFO_GOODS_ID_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_NAME_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_SEX_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_LEVEL_LIMIT_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_CLUB_VIP_LEVEL_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_QB_2_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_CONSUME_TYPE_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_TIME_UNIT_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_IS_ONLINE_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_CAN_BUY_FIELD = protobuf.FieldDescriptor();
local GOODS_INFO_ARRAY = protobuf.Descriptor();
local GOODS_INFO_ARRAY_ITEMS_FIELD = protobuf.FieldDescriptor();

GOODS_INFO_GOODS_ID_FIELD.name = "goods_id"
GOODS_INFO_GOODS_ID_FIELD.full_name = ".uFramework.Goods_Info.goods_id"
GOODS_INFO_GOODS_ID_FIELD.number = 1
GOODS_INFO_GOODS_ID_FIELD.index = 0
GOODS_INFO_GOODS_ID_FIELD.label = 1
GOODS_INFO_GOODS_ID_FIELD.has_default_value = false
GOODS_INFO_GOODS_ID_FIELD.default_value = 0
GOODS_INFO_GOODS_ID_FIELD.type = 13
GOODS_INFO_GOODS_ID_FIELD.cpp_type = 3

GOODS_INFO_NAME_FIELD.name = "name"
GOODS_INFO_NAME_FIELD.full_name = ".uFramework.Goods_Info.name"
GOODS_INFO_NAME_FIELD.number = 2
GOODS_INFO_NAME_FIELD.index = 1
GOODS_INFO_NAME_FIELD.label = 1
GOODS_INFO_NAME_FIELD.has_default_value = false
GOODS_INFO_NAME_FIELD.default_value = ""
GOODS_INFO_NAME_FIELD.type = 12
GOODS_INFO_NAME_FIELD.cpp_type = 9

GOODS_INFO_SEX_FIELD.name = "sex"
GOODS_INFO_SEX_FIELD.full_name = ".uFramework.Goods_Info.sex"
GOODS_INFO_SEX_FIELD.number = 3
GOODS_INFO_SEX_FIELD.index = 2
GOODS_INFO_SEX_FIELD.label = 1
GOODS_INFO_SEX_FIELD.has_default_value = false
GOODS_INFO_SEX_FIELD.default_value = 0
GOODS_INFO_SEX_FIELD.type = 13
GOODS_INFO_SEX_FIELD.cpp_type = 3

GOODS_INFO_LEVEL_LIMIT_FIELD.name = "level_limit"
GOODS_INFO_LEVEL_LIMIT_FIELD.full_name = ".uFramework.Goods_Info.level_limit"
GOODS_INFO_LEVEL_LIMIT_FIELD.number = 4
GOODS_INFO_LEVEL_LIMIT_FIELD.index = 3
GOODS_INFO_LEVEL_LIMIT_FIELD.label = 1
GOODS_INFO_LEVEL_LIMIT_FIELD.has_default_value = false
GOODS_INFO_LEVEL_LIMIT_FIELD.default_value = 0
GOODS_INFO_LEVEL_LIMIT_FIELD.type = 13
GOODS_INFO_LEVEL_LIMIT_FIELD.cpp_type = 3

GOODS_INFO_CLUB_VIP_LEVEL_FIELD.name = "club_vip_level"
GOODS_INFO_CLUB_VIP_LEVEL_FIELD.full_name = ".uFramework.Goods_Info.club_vip_level"
GOODS_INFO_CLUB_VIP_LEVEL_FIELD.number = 5
GOODS_INFO_CLUB_VIP_LEVEL_FIELD.index = 4
GOODS_INFO_CLUB_VIP_LEVEL_FIELD.label = 1
GOODS_INFO_CLUB_VIP_LEVEL_FIELD.has_default_value = false
GOODS_INFO_CLUB_VIP_LEVEL_FIELD.default_value = 0
GOODS_INFO_CLUB_VIP_LEVEL_FIELD.type = 5
GOODS_INFO_CLUB_VIP_LEVEL_FIELD.cpp_type = 1

GOODS_INFO_QB_2_FIELD.name = "qb_2"
GOODS_INFO_QB_2_FIELD.full_name = ".uFramework.Goods_Info.qb_2"
GOODS_INFO_QB_2_FIELD.number = 6
GOODS_INFO_QB_2_FIELD.index = 5
GOODS_INFO_QB_2_FIELD.label = 1
GOODS_INFO_QB_2_FIELD.has_default_value = false
GOODS_INFO_QB_2_FIELD.default_value = 0
GOODS_INFO_QB_2_FIELD.type = 5
GOODS_INFO_QB_2_FIELD.cpp_type = 1

GOODS_INFO_CONSUME_TYPE_FIELD.name = "consume_type"
GOODS_INFO_CONSUME_TYPE_FIELD.full_name = ".uFramework.Goods_Info.consume_type"
GOODS_INFO_CONSUME_TYPE_FIELD.number = 7
GOODS_INFO_CONSUME_TYPE_FIELD.index = 6
GOODS_INFO_CONSUME_TYPE_FIELD.label = 1
GOODS_INFO_CONSUME_TYPE_FIELD.has_default_value = false
GOODS_INFO_CONSUME_TYPE_FIELD.default_value = 0
GOODS_INFO_CONSUME_TYPE_FIELD.type = 13
GOODS_INFO_CONSUME_TYPE_FIELD.cpp_type = 3

GOODS_INFO_TIME_UNIT_FIELD.name = "time_unit"
GOODS_INFO_TIME_UNIT_FIELD.full_name = ".uFramework.Goods_Info.time_unit"
GOODS_INFO_TIME_UNIT_FIELD.number = 8
GOODS_INFO_TIME_UNIT_FIELD.index = 7
GOODS_INFO_TIME_UNIT_FIELD.label = 1
GOODS_INFO_TIME_UNIT_FIELD.has_default_value = false
GOODS_INFO_TIME_UNIT_FIELD.default_value = 0
GOODS_INFO_TIME_UNIT_FIELD.type = 13
GOODS_INFO_TIME_UNIT_FIELD.cpp_type = 3

GOODS_INFO_IS_ONLINE_FIELD.name = "is_online"
GOODS_INFO_IS_ONLINE_FIELD.full_name = ".uFramework.Goods_Info.is_online"
GOODS_INFO_IS_ONLINE_FIELD.number = 9
GOODS_INFO_IS_ONLINE_FIELD.index = 8
GOODS_INFO_IS_ONLINE_FIELD.label = 1
GOODS_INFO_IS_ONLINE_FIELD.has_default_value = false
GOODS_INFO_IS_ONLINE_FIELD.default_value = 0
GOODS_INFO_IS_ONLINE_FIELD.type = 13
GOODS_INFO_IS_ONLINE_FIELD.cpp_type = 3

GOODS_INFO_CAN_BUY_FIELD.name = "can_buy"
GOODS_INFO_CAN_BUY_FIELD.full_name = ".uFramework.Goods_Info.can_buy"
GOODS_INFO_CAN_BUY_FIELD.number = 10
GOODS_INFO_CAN_BUY_FIELD.index = 9
GOODS_INFO_CAN_BUY_FIELD.label = 1
GOODS_INFO_CAN_BUY_FIELD.has_default_value = false
GOODS_INFO_CAN_BUY_FIELD.default_value = 0
GOODS_INFO_CAN_BUY_FIELD.type = 13
GOODS_INFO_CAN_BUY_FIELD.cpp_type = 3

GOODS_INFO.name = "Goods_Info"
GOODS_INFO.full_name = ".uFramework.Goods_Info"
GOODS_INFO.nested_types = {}
GOODS_INFO.enum_types = {}
GOODS_INFO.fields = {GOODS_INFO_GOODS_ID_FIELD, GOODS_INFO_NAME_FIELD, GOODS_INFO_SEX_FIELD, GOODS_INFO_LEVEL_LIMIT_FIELD, GOODS_INFO_CLUB_VIP_LEVEL_FIELD, GOODS_INFO_QB_2_FIELD, GOODS_INFO_CONSUME_TYPE_FIELD, GOODS_INFO_TIME_UNIT_FIELD, GOODS_INFO_IS_ONLINE_FIELD, GOODS_INFO_CAN_BUY_FIELD}
GOODS_INFO.is_extendable = false
GOODS_INFO.extensions = {}
GOODS_INFO_ARRAY_ITEMS_FIELD.name = "items"
GOODS_INFO_ARRAY_ITEMS_FIELD.full_name = ".uFramework.Goods_Info_Array.items"
GOODS_INFO_ARRAY_ITEMS_FIELD.number = 1
GOODS_INFO_ARRAY_ITEMS_FIELD.index = 0
GOODS_INFO_ARRAY_ITEMS_FIELD.label = 3
GOODS_INFO_ARRAY_ITEMS_FIELD.has_default_value = false
GOODS_INFO_ARRAY_ITEMS_FIELD.default_value = {}
GOODS_INFO_ARRAY_ITEMS_FIELD.message_type = GOODS_INFO
GOODS_INFO_ARRAY_ITEMS_FIELD.type = 11
GOODS_INFO_ARRAY_ITEMS_FIELD.cpp_type = 10

GOODS_INFO_ARRAY.name = "Goods_Info_Array"
GOODS_INFO_ARRAY.full_name = ".uFramework.Goods_Info_Array"
GOODS_INFO_ARRAY.nested_types = {}
GOODS_INFO_ARRAY.enum_types = {}
GOODS_INFO_ARRAY.fields = {GOODS_INFO_ARRAY_ITEMS_FIELD}
GOODS_INFO_ARRAY.is_extendable = false
GOODS_INFO_ARRAY.extensions = {}

Goods_Info = protobuf.Message(GOODS_INFO)
Goods_Info_Array = protobuf.Message(GOODS_INFO_ARRAY)

