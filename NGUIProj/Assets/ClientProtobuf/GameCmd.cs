// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: GameCmd.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace GameCmd {

  /// <summary>Holder for reflection information generated from GameCmd.proto</summary>
  public static partial class GameCmdReflection {

    #region Descriptor
    /// <summary>File descriptor for GameCmd.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GameCmdReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1HYW1lQ21kLnByb3RvEghnYW1lX2NtZCroBQoHR2FtZUNtZBIMCghDTURf",
            "QkFTRRAAEhIKDUxPR0lOX0NNRF9NSU4QkE4SCgoFTE9HSU4QkU4SDwoKTE9H",
            "SU5fUkVTUBCSThIPCgpDSEVDS19OQU1FEJNOEhQKD0NIRUNLX05BTUVfUkVT",
            "UBCUThISCg1DUkVBVEVfUExBWUVSEJVOEhcKEkNSRUFURV9QTEFZRVJfUkVT",
            "UBCWThINCghPRkZfTElORRCYThINCghSRUdJU1RFUhCaThISCg1SRUdJU1RF",
            "Ul9SRVNQEJtOEg8KCkxPR0lOX1RFU1QQoU4SEgoNTE9HSU5fQ01EX01BWBDz",
            "ThITCg5QTEFZRVJfQ01EX01JThDcVhITCg5QTEFZRVJfQ01EX01BWBCjWBIR",
            "CgxJVEVNX0NNRF9NSU4QpFgSEQoMSVRFTV9DTURfTUFYEIdZEhMKDlNPQ0lB",
            "TF9DTURfTUlOEPxcEhMKDlNPQ0lBTF9DTURfTUFYEN9dEhQKD01JU1NJT05f",
            "Q01EX01JThCAZBIUCg9NSVNTSU9OX0NNRF9NQVgQ42QSFQoPQVNLX0NSRUFU",
            "RV9ST09NEKCcARIaChRBU0tfQ1JFQVRFX1JPT01fUkVTUBChnAESEgoMQVNL",
            "X0FERF9ST09NEKKcARIXChFBU0tfQUREX1JPT01fUkVTUBCjnAESFAoOQVNL",
            "X0xFQVZFX1JPT00QpJwBEhQKDkFTS19SRUFEWV9ST09NEKWcARIZChNBU0tf",
            "UkVBRFlfUk9PTV9SRVNQEKacARIVCg9BU0tfQ0FOQ0VMX1JPT00Qp5wBEhQK",
            "DkFTS19TVEFSVF9ST09NEKicARIWChBBU0tfUkVFTlRFUl9ST09NEKmcARIX",
            "ChFTVEFSVF9CQVRUTEVfUkVTUBCqnAESGgoUQVNLX1VQREFURV9VU0VSX0RB",
            "VEEQq5wBEhwKFlVQREFURV9VU0VSU19EQVRBX1JFU1AQrJwBEg4KCFJPT01f",
            "TUFYEOidAWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::GameCmd.GameCmd), }, null));
    }
    #endregion

  }
  #region Enums
  public enum GameCmd {
    [pbr::OriginalName("CMD_BASE")] CmdBase = 0,
    /// <summary>
    ///登录模块----------------------------------------------------------------------------------------------------------------------------------------
    /// </summary>
    [pbr::OriginalName("LOGIN_CMD_MIN")] LoginCmdMin = 10000,
    /// <summary>
    /// logic.Login 客户端登录
    /// </summary>
    [pbr::OriginalName("LOGIN")] Login = 10001,
    /// <summary>
    /// logic.LoginResp 登录返回   
    /// </summary>
    [pbr::OriginalName("LOGIN_RESP")] LoginResp = 10002,
    /// <summary>
    /// logic.CheckName 检查名字能否注册
    /// </summary>
    [pbr::OriginalName("CHECK_NAME")] CheckName = 10003,
    /// <summary>
    /// null 没有消息体，检查消息头error_code，0是成功，否则是GameError枚举 
    /// </summary>
    [pbr::OriginalName("CHECK_NAME_RESP")] CheckNameResp = 10004,
    /// <summary>
    /// logic.CreatePlayer 创建角色
    /// </summary>
    [pbr::OriginalName("CREATE_PLAYER")] CreatePlayer = 10005,
    /// <summary>
    /// null 没有消息体，检查消息头error_code，0是成功，否则是GameError枚举 
    /// </summary>
    [pbr::OriginalName("CREATE_PLAYER_RESP")] CreatePlayerResp = 10006,
    /// <summary>
    /// null 玩家离线，服务器用，客户端直接关闭网络连接触发	
    /// </summary>
    [pbr::OriginalName("OFF_LINE")] OffLine = 10008,
    /// <summary>
    /// 注册
    /// </summary>
    [pbr::OriginalName("REGISTER")] Register = 10010,
    /// <summary>
    /// 注册返回
    /// </summary>
    [pbr::OriginalName("REGISTER_RESP")] RegisterResp = 10011,
    /// <summary>
    /// 协议测试
    /// </summary>
    [pbr::OriginalName("LOGIN_TEST")] LoginTest = 10017,
    /// <summary>
    /// 消息分类临界
    /// </summary>
    [pbr::OriginalName("LOGIN_CMD_MAX")] LoginCmdMax = 10099,
    /// <summary>
    ///玩家整体模块------------------------------------------------------------------------------------------------------------------------------------
    /// </summary>
    [pbr::OriginalName("PLAYER_CMD_MIN")] PlayerCmdMin = 11100,
    /// <summary>
    /// 消息分类临界
    /// </summary>
    [pbr::OriginalName("PLAYER_CMD_MAX")] PlayerCmdMax = 11299,
    /// <summary>
    ///道具管理系统-----------------------------------------------------------------------------------------------------------------------
    /// </summary>
    [pbr::OriginalName("ITEM_CMD_MIN")] ItemCmdMin = 11300,
    /// <summary>
    ///-----------------------------------------------------------------------------------------------------------------------------------------------
    /// </summary>
    [pbr::OriginalName("ITEM_CMD_MAX")] ItemCmdMax = 11399,
    /// <summary>
    ///好友，聊天系统--------------------------------------------------------------------------------------------------------------------------------
    /// </summary>
    [pbr::OriginalName("SOCIAL_CMD_MIN")] SocialCmdMin = 11900,
    /// <summary>
    ///----------------------------------------------------------------------------------------------------------------------------------------------------
    /// </summary>
    [pbr::OriginalName("SOCIAL_CMD_MAX")] SocialCmdMax = 11999,
    /// <summary>
    ///任务系统-----------------------------------------------------------------------------------------------------------
    /// </summary>
    [pbr::OriginalName("MISSION_CMD_MIN")] MissionCmdMin = 12800,
    /// <summary>
    ///------------------------------------------------------------------------------------------------------------------
    /// </summary>
    [pbr::OriginalName("MISSION_CMD_MAX")] MissionCmdMax = 12899,
    /// <summary>
    ///---房间模块
    /// </summary>
    [pbr::OriginalName("ASK_CREATE_ROOM")] AskCreateRoom = 20000,
    /// <summary>
    ///放回
    /// </summary>
    [pbr::OriginalName("ASK_CREATE_ROOM_RESP")] AskCreateRoomResp = 20001,
    /// <summary>
    ///加入房间
    /// </summary>
    [pbr::OriginalName("ASK_ADD_ROOM")] AskAddRoom = 20002,
    [pbr::OriginalName("ASK_ADD_ROOM_RESP")] AskAddRoomResp = 20003,
    /// <summary>
    ///离开房间
    /// </summary>
    [pbr::OriginalName("ASK_LEAVE_ROOM")] AskLeaveRoom = 20004,
    /// <summary>
    ///准备
    /// </summary>
    [pbr::OriginalName("ASK_READY_ROOM")] AskReadyRoom = 20005,
    [pbr::OriginalName("ASK_READY_ROOM_RESP")] AskReadyRoomResp = 20006,
    /// <summary>
    ///取消
    /// </summary>
    [pbr::OriginalName("ASK_CANCEL_ROOM")] AskCancelRoom = 20007,
    /// <summary>
    ///开始游戏
    /// </summary>
    [pbr::OriginalName("ASK_START_ROOM")] AskStartRoom = 20008,
    /// <summary>
    ///重进房间
    /// </summary>
    [pbr::OriginalName("ASK_REENTER_ROOM")] AskReenterRoom = 20009,
    [pbr::OriginalName("START_BATTLE_RESP")] StartBattleResp = 20010,
    [pbr::OriginalName("ASK_UPDATE_USER_DATA")] AskUpdateUserData = 20011,
    [pbr::OriginalName("UPDATE_USERS_DATA_RESP")] UpdateUsersDataResp = 20012,
    [pbr::OriginalName("ROOM_MAX")] RoomMax = 20200,
  }

  #endregion

}

#endregion Designer generated code
