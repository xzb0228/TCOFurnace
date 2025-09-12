using System;
using System.Data.SQLite;
using System.IO;
using TCOFurnace.Models;
using TCOFurnace.Common;
using static log4net.Appender.RollingFileAppender;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Common;

namespace TCOFurnace.DataService
{
    internal class DatabaseUpgrader
    {
        //数据库当前版本
        private static int Version = 0;

        // 私有化构造函数：阻止外部实例化
        private DatabaseUpgrader() { }

        /// <summary>
        /// 数据库升级
        /// </summary>
        public static bool Upgrader()
        {
            try
            {
                bool BInitialize = true;
                //没有数据库一次创建
                if (!File.Exists(SqliteHelper.DataSourc))
                {
                    BInitialize = Initialize();
                }

                //创建数据库失败
                if (!BInitialize)
                {
                    return BInitialize;
                }

                int.TryParse(SqliteHelper.ExecuteScalar($"SELECT max( DbVersion ) FROM VersionInfo ").ToString(),out Version)  ;

                // 后期数据库表结构变化时 随着版本升级而跟新
                BInitialize = Update();

                return BInitialize;
            }
            catch (Exception ex)
            {
                Loger.Error("数据库升级报错：" + ex.Message);
                return false;
            }


        }
        /// <summary>
        /// 第一次使用数据库时需要建的基础表
        /// </summary>
        private static bool Initialize()
        {
            try
            {
                using (var Connection = SqliteHelper.GetConn())
                {
                    Connection.Open();

                    //建表
                    using (var Transaction = Connection.BeginTransaction()) // 事务确保升级原子性
                    {
                        try
                        {
                            #region 创建数据库版本表
                            string CreateVersionTableSql = @"
                 CREATE TABLE IF NOT EXISTS VersionInfo (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                DbVersion INTEGER NOT NULL,
                UpdateTime DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP);";
                            using (var command = new SQLiteCommand(CreateVersionTableSql, Connection, Transaction))
                            {
                                command.ExecuteNonQuery();
                            }
                            #endregion

                            #region 创建用户表
                            string CreateUsersTableSql = @"
                             CREATE TABLE IF NOT EXISTS Users (
                      Id  INTEGER PRIMARY KEY AUTOINCREMENT,
                UserCode  TEXT NOT NULL,
                UserName  TEXT UNIQUE NOT NULL,
                PassWord  TEXT NOT NULL,
                Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
            )";
                            using (var command = new SQLiteCommand(CreateUsersTableSql, Connection, Transaction))
                            {
                                command.ExecuteNonQuery();
                            }
                            #endregion

                            Loger.Info("数据库表初始化成功");
                            Transaction.Commit(); // 所有升级成功，提交事务
                        }
                        catch (Exception ex)
                        {
                            Loger.Error("数据库表初始化失败:" +ex.Message);
                            Transaction.Rollback();
                            throw;
                        }
                    }

                    //给表插入初始值
                    using (var Transaction = Connection.BeginTransaction()) // 事务确保升级原子性
                    {

                        try
                        {
                            #region 给表添加管理员
                            string initUsersSql = $"INSERT INTO Users (usercode, username,password,created_at,updated_at) VALUES ('1000','admin','{AesEncryptor.EncryptStr("admin@123")}',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP);";
                            using (var command = new SQLiteCommand(initUsersSql, Connection, Transaction))
                            {
                                command.ExecuteNonQuery();
                            }
                            #endregion

                            #region 初始化版本为0（首次创建数据库时）
                            string initVersionSql = @"
                INSERT INTO VersionInfo (DbVersion, UpdateTime)
                VALUES (0, CURRENT_TIMESTAMP);";
                            using (var command = new SQLiteCommand(initVersionSql, Connection, Transaction))
                            {
                                command.ExecuteNonQuery();
                            }
                            #endregion

                            Loger.Info("数据库表数据初始化成功");
                            Transaction.Commit(); // 所有升级成功，提交事务
                        }
                        catch (Exception ex)
                        {
                            Loger.Error("数据库表数据初始化失败:" + ex.Message);
                            Transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //初始话报错删除数据库文件
                File.Delete(SqliteHelper.DataSourc);
                Loger.Error("数据库初始化报凑：" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 后期数据库表结构变化时 随着版本升级而更新
        /// </summary>
        public static bool Update()
        {
            if (Version < 1)
            { 
            
            }
            
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
    }
}
