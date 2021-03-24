CONSUMER_KEY = "nxDE6IdRr9XTSstxMSVjDTvTx"
CONSUMER_SECRET = "QbApeXoYMouEb4P6FAd4j52q3Y4qn2TWIHInxxEvVrVmiJtPTg"
ACCESS_TOKEN = "2474127379-0yulqMvbbhpUiFz3FJlvTQwSuNJbO6bkqVmnucX"
ACCESS_TOKEN_SECRET = "508VIWgn1i4rWRLZS9SvxJgpIVRAI06a7KVDiJVxfbHyB"

import tweepy
import csv
import pprint
import codecs
import pandas as pd

cursor = -1
while cursor != 0:
   auth = tweepy.OAuthHandler(CONSUMER_KEY, CONSUMER_SECRET)
   auth.set_access_token(ACCESS_TOKEN, ACCESS_TOKEN_SECRET)
   api = tweepy.API(auth, wait_on_rate_limit=True)
   itr = tweepy.Cursor(api.followers_ids, id='@BizHack1', cursor=cursor).pages()
   followerDatas = []
   try:
       for follower_ids in itr.next():
           try:
               user = api.get_user(follower_ids)
               pprint.pprint(user._json)
               pprint.pprint(user.followers_count)
               pprint.pprint(user.screen_name)
               pprint.pprint(user.name)
               pprint.pprint(user.profile_image_url_https)
               pprint.pprint(user.description)
               followerData = {}
               followerData["Name"] = user.name
               followerData["ScreenName"] = user.screen_name
               followerData["Follow"] = user.friends_count
               followerData["Follower"] = user.followers_count
               followerData["Description"] = user.description
               followerData["TweetCount"] = user.statuses_count
               followerDatas.append(followerData)
           except tweepy.error.TweepError as e:
               print(e.reason)
       df = pd.DataFrame(followerDatas).loc[:,["Name","ScreenName","Follow","Follower","TweetCount","Description"]]
       df.to_csv( "oono.csv")
       print("test.csv」が作成されました。")
   except ConnectionError as e:
       print(e.reason)
   cursor = itr.next_cursor