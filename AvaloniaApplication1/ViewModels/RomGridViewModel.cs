using AvaloniaApplication1.Models;
using RomVaultCore;
using RomVaultCore.RvDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class RomGridViewModel : ViewModelBase
    {
        /// <summary>
        /// Rows shown in the DataGrid.
        /// </summary>
        public BulkObservableCollection<RomNode> Roms { get; } = new();

        public RomGridViewModel()
        {
        }


        public void Set(RvFile tGame)
        {
            var loaded = new List<RomNode>();

            AddDir(tGame, "", ref loaded);

            Roms.Reset(loaded);
        }


        private void AddDir(RvFile tGame, string pathAdd, ref List<RomNode> fileList)
        {
            if (tGame == null)
                return;

            try
            {

                for (int l = 0; l < tGame.ChildCount; l++)
                {
                    RvFile tBase = tGame.Child(l);

                    RvFile tFile = tBase;
                    if (tFile.IsFile)
                    {
                        AddRom(tFile, pathAdd, ref fileList);
                    }

                    if (tGame.Dat == null)
                    {
                        continue;
                    }

                    RvFile tDir = tBase;
                    if (!tDir.IsDirectory)
                    {
                        continue;
                    }

                    if (tDir.Game == null)
                    {
                        AddDir(tDir, pathAdd + tDir.Name + "/", ref fileList);
                    }
                }
            }
            catch { }
        }


        private void AddRom(RvFile tFile, string pathAdd, ref List<RomNode> fileList)
        {
            try
            {
                if (tFile.DatStatus != DatStatus.InDatMerged || tFile.RepStatus != RepStatus.NotCollected || Settings.rvSettings.chkBoxShowMerged)
                {
                    tFile.UiDisplayName = pathAdd + tFile.Name;
                    fileList.Add(new RomNode(tFile));
                }
            }
            catch { }
        }
    }
}
