def Read_Data(file_path):
    albums = []
    with open(file_path, "r") as file:
        while True:
            band_name = file.readline().strip()
            if not band_name:
                break

            album_title = file.readline().strip()
            track_count = int(file.readline().strip())
            release_year = int(file.readline().strip())
            downloads = int(file.readline().strip())

            album = {
                "band_name": band_name,
                "album_title": album_title,
                "track_count": track_count,
                "release_year": release_year,
                "downloads": downloads
            }
            albums.append(album)
            file.readline()
        
    return albums

def Display_Data(albums):
    for album in albums:
        print(f"{album['band_name']}")
        print(f"{album['album_title']}")
        print(f"{album['track_count']}")
        print(f"{album['release_year']}")
        print(f"{album['downloads']}")
        print("")

data_file = "Data.txt"
albums_data = Read_Data(data_file)

if albums_data:
    Display_Data(albums_data)
else:
    print("Brak danych do wyświetlenia.")
