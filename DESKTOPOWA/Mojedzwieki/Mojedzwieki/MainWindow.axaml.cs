using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Mojedzwieki;

public partial class MainWindow : Window
{
    public List<Album> albums;
    public int currentIndex;
    public string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.txt");


    private List<Album> LoadAlbums(string filePath)
    {
        List<Album> loadedAlbums = new List<Album>();
        
        if (!File.Exists(filePath)) return loadedAlbums;
        
        var lines = File.ReadAllLines(filePath);


        for (int i = 0; i < lines.Length; i++)
        {
            if (i + 4 < lines.Length)
            {
                var bandName = lines[i].Trim();
                var title = lines[i + 1].Trim();
                
                int trackCount = 0;
                int releaseDate = 0;
                int downloads = 0;

                if (int.TryParse(lines[i + 2].Trim(), out trackCount) &&
                    int.TryParse(lines[i + 3].Trim(), out releaseDate) &&
                    int.TryParse(lines[i + 4].Trim(), out downloads))
                {
                    loadedAlbums.Add(new Album
                    {
                        BandName = bandName,
                        Title = title,
                        TrackCount = trackCount,
                        ReleaseDate = releaseDate,
                        Downloads = downloads
                    });
                }
            }
        }
        return loadedAlbums;
    }

    public MainWindow()
    {
        InitializeComponent();
        

        albums = LoadAlbums(filePath);
        currentIndex = 0;
        

        DisplayAlbum();
    }


    private void DisplayAlbum()
    {
        var album = albums[currentIndex];

        bandName.Text = album.BandName;
        title.Text = album.Title;
        trackCount.Text = album.TrackCount.ToString();
        releaseDate.Text = album.ReleaseDate.ToString();
        downloads.Text = album.Downloads.ToString();
        Console.WriteLine($"{album.BandName.Trim()} {album.Title.Trim()} {album.TrackCount.ToString().Trim()} {album.ReleaseDate.ToString().Trim()} {album.Downloads.ToString().Trim()}");
    }


    public void Previous(object sender, PointerPressedEventArgs e)
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = albums.Count - 1;
        }
        DisplayAlbum();
    }


    private void Next(object? sender, PointerPressedEventArgs e)
    {
        currentIndex++;
        if (currentIndex >= albums.Count)
        {
            currentIndex = 0;
        }
        DisplayAlbum();
    }


    private void Download(object? sender, RoutedEventArgs e)
    {
        var album = albums[currentIndex];
        album.Downloads++;
        
        downloads.Text = album.Downloads.ToString();
    }
}


public class Album
{
    public string BandName { get; set; }
    public string Title { get; set; }
    public int TrackCount { get; set; }
    public int ReleaseDate { get; set; }
    public int Downloads { get; set; }
}
