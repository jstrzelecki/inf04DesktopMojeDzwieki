using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform;

namespace MojeDzwiekiInf04;

public partial class MainWindow : Window
{ 
    private List<Album> _albums = new();
    private int _albumIndex = 0;
    public MainWindow()
    {
        InitializeComponent();
       // ReadDataFromTxtFile("Data.txt");
        ReadDataFromAvaloniaResource("avares://MojeDzwiekiInf04/Assets/Data.txt");
        if (_albums.Count > 0)
        {
            ShowOneRecord(_albums[_albumIndex]);
        }
    }

    private void ReadDataFromAvaloniaResource(string assetPath)
    {
        var uri = new Uri(assetPath);
        using Stream stream = AssetLoader.Open(uri);
        using (StreamReader reader = new StreamReader(stream))
        {
            var lines = new List<string>();
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }
            MakeListOfAlbums(lines);
            
        }
    }

    private void ShowOneRecord(Album album)
    {
        ArtistLabel.Content = album.Artist;
        AlbumLabel.Content = album.Title;
        TracksNumberLabel.Content = album.TracksNumber;
        ReleasedYearLabel.Content = album.Year;
        DownloadNumberLabel.Content = album.Downloads;
    }

    private void ReadDataFromTxtFile(string fileName)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, fileName);
        
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            MakeListOfAlbums(lines.ToList()); 
           
           
        }
        else
        {
            Console.WriteLine("NIE znaleziono plik");
        }
    }


    private void DownloadButton(object? sender, RoutedEventArgs e)
    {
        _albums[_albumIndex].Downloads++;
        DownloadNumberLabel.Content = _albums[_albumIndex].Downloads;
    }

    private void BackWardButton(object? sender, RoutedEventArgs e)
    {
       _albumIndex = (_albumIndex - 1 + _albums.Count) % _albums.Count; 
       
        ShowOneRecord(_albums[_albumIndex]); 
    }

    private void ForwardButton(object? sender, RoutedEventArgs e)
    {
        _albumIndex = (_albumIndex + 1) % _albums.Count; 
       
        ShowOneRecord(_albums[_albumIndex]); 
    }

    private void MakeListOfAlbums(List<string> dataFromFile)
    {
         for (int i = 0; i < dataFromFile.Count; i += 6)
         {
             try 
             {
                 var album = new Album
                 {
                     Artist = dataFromFile[i],
                     Title = dataFromFile[i + 1],
                     TracksNumber = int.Parse(dataFromFile[i + 2]),
                     Year = int.Parse(dataFromFile[i + 3]),
                     Downloads =  int.Parse(dataFromFile[i + 4]),
                 };
                 _albums.Add(album);
                           
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message); 
             }
         }
    }
}