using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ServerManager.DatabaseControl
{

    public partial class Character : MetroWindow
    {
        public Character()
        {
            InitializeComponent();
        }

        getInfos get = new getInfos();

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //If dont need to find but i do due to protection level, and i set the values to textbox for user can edit
        public bool searchCharacter(string s)
        {
            using (var con = new SqlConnection(get.cn))
            {
                try
                {
                    con.Open();
                    string c = "Select * FROM CHARACTER.dbo.user_character WHERE character_name=@k";
                    using (var cmd = new SqlCommand(c, con))
                    {
                        cmd.Parameters.AddWithValue("@k", s);
                        SqlDataReader re = cmd.ExecuteReader();
                        while (re.Read())
                        {
                            //show by what i want fom db using the respective name such i wanna know the user_no, etc and show in textbox so i'll return 
                            charname.Text = re["character_name"].ToString();
                            charnumber.Text = re["character_no"].ToString();
                            usernumber.Text = re["user_no"].ToString();
                            advpoints.Text = re["dwAdv"].ToString();
                            dwpeerage.Text = re["dwPeerage"].ToString();
                            exp.Text = re["dwEXP"].ToString();
                            money.Text = re["dwMoney"].ToString();
                            storemoney.Text = re["dwStoreMoney"].ToString();
                            storagemoney.Text = re["dwStorageMoney"].ToString();
                            hp.Text = re["nHP"].ToString();
                            mp.Text = re["nMP"].ToString();
                            str.Text = re["wStr"].ToString();
                            dex.Text = re["wDex"].ToString();
                            wcon.Text = re["wCon"].ToString();
                            spr.Text = re["wSpr"].ToString();
                            posx.Text = re["wPosX"].ToString();
                            posy.Text = re["wPosY"].ToString();
                            retposx.Text = re["wRetPosX"].ToString();
                            retposy.Text = re["wRetPosY"].ToString();
                            mapindex.Text = re["wMapIndex"].ToString();
                            retmapindex.Text = re["wRetMapIndex"].ToString();
                            statpoint.Text = re["wStatPoint"].ToString();
                            skillpoint.Text = re["wSkillPoint"].ToString();
                            level.Text = re["wLevel"].ToString();
                            pcclass.Text = re["byPCClass"].ToString();
                            direction.Text = re["byDirection"].ToString();
                            retdirection.Text = re["byRetDirection"].ToString();
                            skillclearcount.Text = re["bySkillClearCount"].ToString();
                            statclearcount.Text = re["byStatClearCount"].ToString();
                            pkcount.Text = re["wPKCount"].ToString();
                            chaoticlevel.Text = re["wChaoticLevel"].ToString();
                            shield.Text = re["nShield"].ToString();
                            flag.Text = re["dwFlag"].ToString();
                            loginflag.Text = re["login_flag"].ToString();
                            pvppoint.Text = re["dwPVPPoint"].ToString();
                            winrecord.Text = re["wWinRecord"].ToString();
                            loserecord.Text = re["wLoseRecord"].ToString();
                            drawrecord.Text = re["wDrawRecord"].ToString();
                            supplypoint.Text = re["dwSupplyPoint"].ToString();
                            storagecount.Text = re["wStorageCount"].ToString();
                            divisionexp.Text = re["dwDivisionExp"].ToString();
                            storeexpansion.Text = re["byStoreExpansion"].ToString();
                            awakening.Text = re["bAwakening"].ToString();
                            divisiongrade.Text = re["wDivisionGrade"].ToString();
                            grade.Text = re["wGrade"].ToString();
                            ppoints.Text = re["dwLowPPoint"].ToString();


                        }

                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            return false;
        }

        //Update whole rows, its such a big querry..
        private async void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] theBytes = Encoding.UTF8.GetBytes("0");
            using (var con = new SqlConnection(get.cn))
            {
                try
                {
                    con.Open();
                    string c = "UPDATE CHARACTER.dbo.user_character SET character_no=@charno, character_name=@charname, user_no=@userno, dwAdv=@adv ,dwPeerage=@peerage, dwExp=@exp, dwMoney=@money, dwStoreMoney=@storemoney, dwStorageMoney=@storagemoney, nHP=@hp, nMP=@mp, wStr=@str, wDex=@dex, wCon=@con ,wSpr=@spr, wPosX=@posx, wPosY=@posy, wRetPosX=@retposx, wRetPosY=@retposy, wMapIndex=@mapindex, wRetMapIndex=@retmapindex, wStatPoint=@statpoint, wSkillPoint=@skillpoint, wLevel=@level, byPCClass=@pcclass, byDirection=@bydir, byRetDirection=@byretdir, bySkillClearCount=@skillclearcount, byStatClearCount=@statclearcount, wPKCount=@pkcount, wChaoticLevel=@chaoticlevel, nShield=@shield, dwFlag=@flag, login_flag=@loginflag, ipt_date=@iptdate, ipt_time=@ipttime, upt_time=@upttime, login_time=@logintime, logout_time=@logouttime, user_ip_addr=@userip, dwPVPPoint=@pvppoint, wWinRecord=@winrecord, wLoseRecord=@loserecord, wDrawRecord=@drawrecord, dwSupplyPoint=@supplypoint, wStorageCount=@storagecount, wSuitLockFlag=@suitflag , dwPunAccumulation=@dwpu, byPetFlag=@petflag, dwKaronEventPoint=@karonevent, dwCherubimEventPoint=@cherubimevent, dwDivisionExp=@divisionexp, bAwakening=@awake, wDivisionGrade=@divisiongrade, wGrade=@grade, dwHiAllRExp=@hiall, byStoreExpansion=@storeexpansion ,dwLowAllRExp=@lowall, dwHiAllRPoint=@hiallpoint, dwLowAllRPoint=@lowallpoint, dwHiCombatRPoint=@combatpoint, dwLowCombatRPoint=@lowcombatpoint, dwCombatWin=@combatwin, dwCombatDrow=@combatdrow, dwCombatLose=@combatlose, dwHiPPoint=@hippoint, dwLowPPoint=@lowppoint, bSaleTag=@saletag, byRebirthFlag=@rebirthflag WHERE character_no=@charno";

                    using (var cmd = new SqlCommand(c, con))
                    {
                        cmd.Parameters.AddWithValue("@charno", charnumber.Text);
                        cmd.Parameters.AddWithValue("@charname", charname.Text);
                        cmd.Parameters.AddWithValue("@userno", usernumber.Text);
                        cmd.Parameters.AddWithValue("@adv", advpoints.Text);
                        cmd.Parameters.AddWithValue("@peerage", dwpeerage.Text);
                        cmd.Parameters.AddWithValue("@exp", exp.Text);
                        cmd.Parameters.AddWithValue("@money", money.Text);
                        cmd.Parameters.AddWithValue("@storemoney", storemoney.Text);
                        cmd.Parameters.AddWithValue("@storagemoney", storagemoney.Text);
                        cmd.Parameters.AddWithValue("@hp", hp.Text);
                        cmd.Parameters.AddWithValue("@mp", mp.Text);
                        cmd.Parameters.AddWithValue("@str", str.Text);
                        cmd.Parameters.AddWithValue("@dex", dex.Text);
                        cmd.Parameters.AddWithValue("@con", wcon.Text);
                        cmd.Parameters.AddWithValue("@spr", spr.Text);
                        cmd.Parameters.AddWithValue("@posx", posx.Text);
                        cmd.Parameters.AddWithValue("@posy", posy.Text);
                        cmd.Parameters.AddWithValue("@retposx", retposx.Text);
                        cmd.Parameters.AddWithValue("@retposy", retposy.Text);
                        cmd.Parameters.AddWithValue("@mapindex", mapindex.Text);
                        cmd.Parameters.AddWithValue("@retmapindex", retmapindex.Text);
                        cmd.Parameters.AddWithValue("@statpoint", statpoint.Text);
                        cmd.Parameters.AddWithValue("@skillpoint", skillpoint.Text);
                        cmd.Parameters.AddWithValue("@level", level.Text);
                        cmd.Parameters.AddWithValue("@pcclass", pcclass.Text);
                        cmd.Parameters.AddWithValue("@bydir", direction.Text);
                        cmd.Parameters.AddWithValue("@byretdir", retdirection.Text);
                        cmd.Parameters.AddWithValue("@skillclearcount", skillclearcount.Text);
                        cmd.Parameters.AddWithValue("@statclearcount", statclearcount.Text);
                        cmd.Parameters.AddWithValue("@pkcount", pkcount.Text);
                        cmd.Parameters.AddWithValue("@chaoticlevel", chaoticlevel.Text);
                        cmd.Parameters.AddWithValue("@shield", shield.Text);
                        cmd.Parameters.AddWithValue("@flag", flag.Text);
                        cmd.Parameters.AddWithValue("@loginflag", loginflag.Text);
                        cmd.Parameters.AddWithValue("@iptdate", 20050325);
                        cmd.Parameters.AddWithValue("@ipttime", SqlDbType.DateTime).Value = DateTime.Today;
                        cmd.Parameters.AddWithValue("@upttime", SqlDbType.DateTime).Value = DateTime.Today;
                        cmd.Parameters.AddWithValue("@logintime", SqlDbType.DateTime).Value = DateTime.Today;
                        cmd.Parameters.AddWithValue("@logouttime", SqlDbType.DateTime).Value = DateTime.Today;
                        cmd.Parameters.AddWithValue("@userip", theBytes[0]);
                        cmd.Parameters.AddWithValue("@pvppoint", pvppoint.Text);
                        cmd.Parameters.AddWithValue("@winrecord", winrecord.Text);
                        cmd.Parameters.AddWithValue("@loserecord", loserecord.Text);
                        cmd.Parameters.AddWithValue("@drawrecord", drawrecord.Text);
                        cmd.Parameters.AddWithValue("@supplypoint", supplypoint.Text);
                        cmd.Parameters.AddWithValue("@storagecount", storagecount.Text);
                        cmd.Parameters.AddWithValue("@suitflag", 0);
                        cmd.Parameters.AddWithValue("@dwpu", 0);
                        cmd.Parameters.AddWithValue("@petflag", 0);
                        cmd.Parameters.AddWithValue("@karonevent", 0);
                        cmd.Parameters.AddWithValue("@cherubimevent", 0);
                        cmd.Parameters.AddWithValue("@divisionexp", divisionexp.Text);
                        cmd.Parameters.AddWithValue("@storeexpansion", storeexpansion.Text);
                        cmd.Parameters.AddWithValue("@grade", grade.Text);
                        cmd.Parameters.AddWithValue("@hiall", 0);
                        cmd.Parameters.AddWithValue("@lowall", 0);
                        cmd.Parameters.AddWithValue("@hiallpoint", 0);
                        cmd.Parameters.AddWithValue("@lowallpoint", 0);
                        cmd.Parameters.AddWithValue("@combatpoint", 0);
                        cmd.Parameters.AddWithValue("@lowcombatpoint", 0);
                        cmd.Parameters.AddWithValue("@combatwin", 0);
                        cmd.Parameters.AddWithValue("@combatdrow", 0);
                        cmd.Parameters.AddWithValue("@combatlose", 0);
                        cmd.Parameters.AddWithValue("@hippoint", 0);
                        cmd.Parameters.AddWithValue("@lowppoint", ppoints.Text);
                        cmd.Parameters.AddWithValue("@saletag", 0);
                        cmd.Parameters.AddWithValue("@awake", awakening.Text);
                        cmd.Parameters.AddWithValue("@divisiongrade", divisiongrade.Text);
                        cmd.Parameters.AddWithValue("@rebirthflag", 1);

                        //Now execute
                        cmd.ExecuteNonQuery();
                        con.Close();
                        await this.ShowMessageAsync("Character updated", "Your character: [" + charname.Text +"] was been updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

    }
}

