# coding:utf-8
import gdata.youtube
import gdata.youtube.service

yt_service = gdata.youtube.service.YouTubeService()
yt_service.ssl = True

# a typical playlist URI
playlist_uri = "http://gdata.youtube.com/feeds/api/playlists/UCuAit6nWvaKk0XydeUQRbvA"

playlist_video_feed = yt_service.GetYouTubePlaylistVideoFeed(playlist_uri)

# iterate through the feed as you would with any other
for playlist_video_entry in playlist_video_feed.entry:
    print playlist_video_entry.title.text
