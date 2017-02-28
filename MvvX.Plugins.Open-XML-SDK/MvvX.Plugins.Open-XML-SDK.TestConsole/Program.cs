﻿using System;
using System.IO;
using MvvX.Plugins.OpenXMLSDK.Word;
using System.Collections.Generic;
using MvvX.Plugins.OpenXMLSDK.Word.Tables.Models;
using MvvX.Plugins.OpenXMLSDK.Word.Tables;
using MvvX.Plugins.OpenXMLSDK.Word;
using MvvX.Plugins.OpenXMLSDK.Word.Paragraphs;
using System.Diagnostics;
using MvvX.Plugins.OpenXMLSDK.Word.Models;
using MvvmCross.Platform;
using MvvX.Plugins.OpenXMLSDK.Platform.Word;

namespace MvvX.Plugins.OpenXMLSDK.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var resourceName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Global.docx");

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Results")))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Results"));

            string finalFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Results", "FinalDoc_Test_OrientationParagraph-" + DateTime.Now.ToFileTime() + ".docx");
            using (IWordManager word = new WordManager())
            {
                // TODO for debug : use your test file :
                word.OpenDocFromTemplate(resourceName, finalFilePath, true);

                //    word.SaveDoc();
                //    word.CloseDoc();
                //}
                // Insertion de texte dans un bookmark
                // wordManager.SetTextOnBookmark("Insert_Documents", "Hi !");

                // test subtemplate
                using (IWordManager subWord = new WordManager())
                {
                    subWord.OpenDocFromTemplate(resourceName);
                    // test insert html
                    subWord.SetHtmlOnBookmark("Insert_Documents", "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Untitled</title><style type=\"text/css\">\r\np { margin-top: 0px;margin-bottom: 12px;line-height: 1.15; } \r\nbody { font-family: 'Arial';font-style: Normal;font-weight: normal;font-size: 12px; } \r\n.Normal { telerik-style-type: paragraph;telerik-style-name: Normal;border-collapse: collapse; } \r\n.TableNormal { telerik-style-type: table;telerik-style-name: TableNormal;border-collapse: collapse; } \r\n.s_CDEA781A { telerik-style-type: local;font-family: 'Arial';font-style: Normal;font-weight: bold;font-size: 12px;color: #000000; } \r\n.s_1E7640DD { telerik-style-type: local;font-family: 'Arial';font-style: Normal;font-weight: normal;font-size: 12px;color: #000000; } \r\n.p_80A10895 { telerik-style-type: local;margin-left: 24px;text-indent: 0px; } \r\n.p_6CC438D { telerik-style-type: local;margin-right: 0px;margin-left: 24px;text-indent: 0px; } \r\n.s_A9E8602F { telerik-style-type: local;font-family: 'Arial';font-style: Italic;font-weight: bold;font-size: 12px;color: #000000; } \r\n.s_242FFA2F { telerik-style-type: local;font-family: 'Arial';font-style: Italic;font-weight: normal;font-size: 12px;color: #000000; } \r\n.s_46C5A272 { telerik-style-type: local;font-family: 'Arial';font-style: Italic;font-weight: normal;font-size: 12px;color: #000000;text-decoration: underline; } \r\n.s_D02E313C { telerik-style-type: local;font-family: 'Arial';font-style: Normal;font-weight: normal;font-size: 12px;color: #000000;text-decoration: underline; } \r\n.p_5A0704CA { telerik-style-type: local;margin-right: 0px;margin-left: 48px;text-indent: 0px; } \r\n.p_146E745D { telerik-style-type: local;margin-right: 0px;margin-left: 72px;text-indent: 0px; } \r\n.s_8795030E { telerik-style-type: local;font-style: Normal;font-weight: normal;text-decoration: underline; } </style></head><body><p class=\"Normal \">Test rich <span class=\"s_CDEA781A\">text </span><span class=\"s_1E7640DD\"></span></p><ul style=\"list-style-type:disc\"><li value=\"1\" class=\"Normal p_80A10895\"><span class=\"s_CDEA781A\">bold</span></li><li value=\"2\" class=\"Normal p_6CC438D\"><span class=\"s_A9E8602F\">italic</span><span class=\"s_242FFA2F\">ddddd</span></li><li value=\"3\" class=\"Normal p_6CC438D\"><span class=\"s_46C5A272\">u</span><span class=\"s_D02E313C\">nderline</span></li><ul style=\"list-style-type:disc\"><li value=\"1\" class=\"Normal p_5A0704CA\"><span class=\"s_D02E313C\">l</span><span class=\"s_1E7640DD\">vl2</span></li><li value=\"2\" class=\"Normal p_5A0704CA\"><span class=\"s_1E7640DD\">hop</span></li><ul style=\"list-style-type:disc\"><li value=\"1\" class=\"Normal p_146E745D\"><span class=\"s_1E7640DD\">lvl3</span></li><li value=\"2\" class=\"Normal p_146E745D\"><span class=\"s_1E7640DD\">...</span><span class=\"s_8795030E\"></span></li></ul></ul></ul></body></html>");
                    subWord.CloseDoc();
                    using (Stream stream = subWord.GetMemoryStream())
                    {
                        word.SetSubDocumentOnBookmark("Insert_Documents", stream);
                    }
                }

                //// Insertion d'une table dans un bookmark
                //// Propriété du Tableau
                //var tableProperty = new TablePropertiesModel()
                //{
                //    TableBorders = new TableBordersModel()
                //    {
                //        TopBorder = new TableBorderModel() { Color = "F7941F", Size = 40, BorderValue = BorderValues.Birds },
                //        LeftBorder = new TableBorderModel() { Color = "CCCCCC", Size = 20, BorderValue = BorderValues.Birds },
                //        RightBorder = new TableBorderModel() { Color = "CCCCCC", Size = 20, BorderValue = BorderValues.Birds },
                //        BottomBorder = new TableBorderModel() { Color = "F7941F", Size = 40, BorderValue = BorderValues.Birds }
                //    },
                //    TableWidth = new TableWidthModel()
                //    {
                //        Width = "5000",
                //        Type = TableWidthUnitValues.Pct
                //    }
                //};
                //// Lignes du premier tableau pour les constats checked
                //var lines = new List<ITableRow>();

                //for (int i = 0; i < 3; i++)
                //{
                //    var borderTopIsOK = new TableBorderModel();
                //    if (i != 0)
                //        borderTopIsOK.BorderValue = BorderValues.Nil;

                //    // Première ligne
                //    var text = word.CreateRunForText("Header Number : " + i,
                //            new RunPropertiesModel()
                //            {
                //                Bold = true,
                //                FontSize = "24",
                //                RunFonts = new RunFontsModel()
                //                {
                //                    Ascii = "Courier New",
                //                    HighAnsi = "Courier New",
                //                    EastAsia = "Courier New",
                //                    ComplexScript = "Courier New"
                //                }
                //            });

                //    var cellules = new List<ITableCell>()
                //    {
                //        word.CreateTableCell(text, new TableCellPropertiesModel() {
                //            Gridspan = new GridSpanModel() { Val = 2 },
                //            Shading = new ShadingModel()
                //            {
                //                Fill = "F7941F"
                //            },
                //            TableCellWidth = new TableCellWidthModel()
                //            {
                //                Width = "8862"
                //            },
                //            TableCellBorders = new TableCellBordersModel()
                //            {
                //                TopBorder = borderTopIsOK
                //            }
                //        }),
                //        word.CreateTableCell(word.CreateRun(), new TableCellPropertiesModel() { 
                //                    TableCellWidth = new TableCellWidthModel()
                //                    {
                //                        Width = "246"
                //                    },
                //                    Shading = new ShadingModel()
                //                    {
                //                        Fill = "F7941F"
                //                    },
                //                    TableCellBorders = new TableCellBordersModel() {
                //                                TopBorder = borderTopIsOK
                //                    }
                //        })
                //    };
                //    lines.Add(word.CreateTableRow(cellules, new TableRowPropertiesModel()
                //    {
                //        TableRowHeight = new TableRowHeightModel()
                //        {
                //            Val = 380
                //        }
                //    }));

                //    // Deuxième ligne
                //    text = word.CreateRunForText("Comments", new RunPropertiesModel() { Bold = true });
                //    cellules = new List<ITableCell>()
                //    {
                //        word.CreateTableCell(word.CreateImage(@"c:\temp\Tulips.jpg", new Drawing.Pictures.Model.PictureModel() {
                //            ImagePartType   = Packaging.ImagePartType.Jpeg,
                //            MaxHeight = 10,
                //            MaxWidth = 500
                //        }), new TableCellPropertiesModel() {
                //                    TableCellWidth = new TableCellWidthModel()
                //                    {
                //                        Width = "4890"
                //                    }
                //        }),
                //        word.CreateTableMergeCell(word.CreateRun(), new TableCellPropertiesModel() {
                //                    Fusion = true,
                //                    TableCellWidth = new TableCellWidthModel()
                //                    {
                //                        Width = "4218"
                //                    },
                //                    Gridspan = new GridSpanModel() { Val = 2 }
                //        })
                //    };
                //    lines.Add(word.CreateTableRow(cellules));

                //    // Troisième ligne
                //    text = word.CreateRunForText("Texte du Constat Number : " + i, new RunPropertiesModel());
                //    cellules = new List<ITableCell>()
                //    {
                //        word.CreateTableCell(text, new TableCellPropertiesModel() {
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4890"
                //                                },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    BottomBorder = new TableBorderModel() {
                //                                        Color = "FF0019"
                //                                        }
                //                                }
                //        }),
                //        word.CreateTableMergeCell(word.CreateRun(), new TableCellPropertiesModel() {
                //                                Fusion = true,
                //                                FusionChild = true,
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4218"
                //                                },
                //                                Gridspan = new GridSpanModel() { Val = 2 },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    BottomBorder = new TableBorderModel() {
                //                                        Color = "FF0019" }
                //                                    }
                //        })
                //    };
                //    lines.Add(word.CreateTableRow(cellules));

                //    // Quatrième ligne
                //    text = word.CreateRunForText("Risques", new RunPropertiesModel() { Bold = true });
                //    cellules = new List<ITableCell>()
                //    {
                //        word.CreateTableCell(text, new TableCellPropertiesModel() {
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4890"
                //                                },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    TopBorder = new TableBorderModel()
                //                                    {
                //                                        Color = "00FF19"
                //                                    }
                //                                }
                //        }),
                //        word.CreateTableMergeCell(word.CreateRun(), new TableCellPropertiesModel() {
                //                                Fusion = true,
                //                                FusionChild = true,
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4218"
                //                                },
                //                                Gridspan = new GridSpanModel() { Val = 2 },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    TopBorder = new TableBorderModel()
                //                                    {
                //                                        Color = "00FF19"
                //                                    }
                //                                }
                //        })
                //    };
                //    lines.Add(word.CreateTableRow(cellules));

                //    // Cinquième ligne
                //    text = word.CreateRunForText("Texte du Risque Number : " + i, new RunPropertiesModel());
                //    cellules = new List<ITableCell>()
                //    {
                //        word.CreateTableCell(text, new TableCellPropertiesModel() {
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4890"
                //                                },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    TopBorder = new TableBorderModel() {
                //                                        BorderValue = BorderValues.Nil }
                //                                }
                //        }),
                //        word.CreateTableMergeCell(word.CreateRun(), new TableCellPropertiesModel() {
                //                                Fusion = true,
                //                                FusionChild = true,
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4218"
                //                                },
                //                                Gridspan = new GridSpanModel() { Val = 2 },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    TopBorder = new TableBorderModel() {
                //                                        BorderValue = BorderValues.Nil }
                //                                    }
                //        })
                //    };
                //    lines.Add(word.CreateTableRow(cellules));

                //    // Sixième ligne
                //    text = word.CreateRunForText("Recommandations", new RunPropertiesModel() { Bold = true });
                //    cellules = new List<ITableCell>()
                //    {
                //        word.CreateTableCell(text, new TableCellPropertiesModel() {
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4890"
                //                                },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    BottomBorder = new TableBorderModel() {
                //                                        BorderValue = BorderValues.Nil }
                //                                    }
                //        }),
                //        word.CreateTableMergeCell(word.CreateRun(), new TableCellPropertiesModel() {
                //                                Fusion = true,
                //                                FusionChild = true,
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4218"
                //                                },
                //                                Gridspan = new GridSpanModel() { Val = 2 },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    BottomBorder = new TableBorderModel() {
                //                                        BorderValue = BorderValues.Nil }
                //                                    }
                //        })
                //    };
                //    lines.Add(word.CreateTableRow(cellules));

                //    var borderBottomIsOK = new TableBorderModel() {
                //        BorderValue = BorderValues.Nil,
                //        Color = "FF0019"
                //    };

                //    if (i == 2)
                //        borderBottomIsOK.BorderValue = BorderValues.Single;

                //    // Septième ligne
                //    text = word.CreateRunForText("Texte de la Recommandation Number : " + i, new RunPropertiesModel());
                //    cellules = new List<ITableCell>()
                //    {
                //        word.CreateTableCell(text, new TableCellPropertiesModel() {
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4890"
                //                                },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    TopBorder = new TableBorderModel() { BorderValue = BorderValues.Nil },
                //                                    BottomBorder = borderBottomIsOK }
                //        }),
                //        word.CreateTableMergeCell(word.CreateRun(), new TableCellPropertiesModel() {
                //                                Fusion = true,
                //                                FusionChild = true,
                //                                TableCellWidth = new TableCellWidthModel()
                //                                {
                //                                    Width = "4218"
                //                                },
                //                                Gridspan = new GridSpanModel() { Val = 2 },
                //                                TableCellBorders = new TableCellBordersModel() {
                //                                    TopBorder = new TableBorderModel() {
                //                                        BorderValue = BorderValues.Nil },
                //                                    BottomBorder = borderBottomIsOK }
                //        })
                //    };
                //    lines.Add(word.CreateTableRow(cellules));
                //}

                //IList<IParagraph> tables = new List<IParagraph>();
                //tables.Add(word.CreateParagraphForRun(word.CreateRunForTable(word.CreateTable(lines, tableProperty))));

                //// Lignes du deuxième tableau pour les constats unchecked
                ////lines = new List<TableRow>();

                //if (tables.Count > 0)
                //    word.SetParagraphsOnBookmark("Insert_Documents", tables);

                word.SaveDoc();
                word.CloseDoc();
            }

            Process.Start(finalFilePath);
        }
    }
}