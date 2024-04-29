using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;

partial class Fusion
{
    private static void FuseDatatables(FileInfo[] PakFiles)
    {
        FileInfo[] UMainAssetFiles = new DirectoryInfo(GlobalVariables.PathMod + @$"\{GlobalVariables.FusionFolderName}\Extracted\{GlobalVariables.FusionPatchFileName}\VotV\Content\main\datatables").GetFiles("*.uasset");

        foreach (FileInfo file in PakFiles)
        {
            FileInfo[] UAssetFiles;

            string path = GlobalVariables.PathExtracted + Path.GetFileNameWithoutExtension(file.Name) + @"\VotV\Content\Mods\" + Path.GetFileNameWithoutExtension(file.Name) + @"\_Content\main\datatables";

            if (Directory.Exists(path))
            {
                UAssetFiles = new DirectoryInfo(path).GetFiles("*.uasset");
            }
            else
            {
                continue;
            }

            foreach (FileInfo uassetFile in UAssetFiles)
            {
                foreach (FileInfo mainUAssetFile in UMainAssetFiles)
                {
                    if (Path.GetFileNameWithoutExtension(mainUAssetFile.Name) == Path.GetFileNameWithoutExtension(uassetFile.Name[1..]))
                    {
                        UAsset mainUAssetHandle = new UAsset(mainUAssetFile.FullName, UAssetAPI.UnrealTypes.EngineVersion.VER_UE4_27);
                        DataTableExport dataTableMain = (DataTableExport)mainUAssetHandle.Exports[0];

                        UAsset uassetHandle = new UAsset(uassetFile.FullName, UAssetAPI.UnrealTypes.EngineVersion.VER_UE4_27);
                        DataTableExport dataTable = (DataTableExport)uassetHandle.Exports[0];

                        mainUAssetHandle.PackageGuid = uassetHandle.PackageGuid;

                        // Adding entries to Name Map
                        foreach (var name in uassetHandle.GetNameMapIndexList())
                        {
                            mainUAssetHandle.AddNameReference(name);
                        }

                        // Adding entries to Import Data
                        int importOffset = mainUAssetHandle.Imports.Count();

                        foreach (var import in uassetHandle.Imports)
                        {
                            import.ClassPackage.Index = mainUAssetHandle.AddNameReference(import.ClassPackage.Value);
                            import.ClassName.Index = mainUAssetHandle.AddNameReference(import.ClassName.Value);
                            import.ObjectName.Index = mainUAssetHandle.AddNameReference(import.ObjectName.Value);

                            if (import.OuterIndex.Index != 0)
                            {
                                import.OuterIndex.Index -= importOffset; // Unsafe
                            }

                            mainUAssetHandle.AddImport(import);
                        }


                        //mainUAssetHandle.Imports.AddRange(uassetHandle.Imports);
                        var EntryMain = dataTableMain.Table.Data[0];

                        foreach (var entry in dataTable.Table.Data)
                        {
                            entry.Name.Index = mainUAssetHandle.AddNameReference(entry.Name.Value);

                            List<PropertyData> listMain = (List<PropertyData>)EntryMain.RawValue;
                            List<PropertyData> list = (List<PropertyData>)entry.RawValue;

                            for (int i = 0; i < listMain.Count; ++i)
                            {
                                if (list[i] is ObjectPropertyData)
                                {
                                    ((ObjectPropertyData)list[i]).Value.Index -= importOffset;  //Unsafe
                                }
                                else if (list[i] is NamePropertyData)
                                {
                                    ((NamePropertyData)list[i]).Value.Index = mainUAssetHandle.AddNameReference(((NamePropertyData)list[i]).Value.Value);
                                }
                                else if (list[i] is BytePropertyData)
                                {
                                    ((BytePropertyData)list[i]).EnumType.Index = mainUAssetHandle.AddNameReference(((BytePropertyData)list[i]).EnumType.Value);
                                    ((BytePropertyData)list[i]).EnumValue.Index = mainUAssetHandle.AddNameReference(((BytePropertyData)list[i]).EnumValue.Value);
                                }

                                list[i].Name = listMain[i].Name;
                            }

                            dataTableMain.Table.Data.Add(entry);
                        }

                        Console.WriteLine(dataTableMain.Table.Data.Count);

                        mainUAssetHandle.Exports[0] = dataTableMain;
                        mainUAssetHandle.Write(mainUAssetFile.FullName);

                        break;
                    }
                }
            }
        }
    }
}
