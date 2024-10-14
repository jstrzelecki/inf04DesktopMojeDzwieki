using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MojeDzwiekiInf04;

public partial class MainWindow : Window
{ 
    private List<Album> _albums = new();
    private int _albumIndex = 0;
    public MainWindow()
    {
        InitializeComponent();
        ReadDataFromTxtFile("Data.txt");
        ShowOneRecord(_albums[_albumIndex]);
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
            for (int i = 0; i < lines.Length; i += 6)
            {
                try
                {
                    var album = new Album
                    {
                        Artist = lines[i],
                        Title = lines[i + 1],
                        TracksNumber = int.Parse(lines[i + 2]),
                        Year = int.Parse(lines[i + 3]),
                        Downloads =  int.Parse(lines[i + 4]),
                    };
                    _albums.Add(album);
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
           
           
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
}