# rufogongora/teslacamviewer
This docker image gives you a friendly interface to navigate your sentry and saved clips from your tesla drive.

Simply map your local tesla clips folder into the `/teslacamdata` folder

Here's an example of my docker-compose file:
```yml
version: "2.1"
services:
  teslacamviewer:
    image:  ghcr.io/rufogongora/teslacamviewer:main
    container_name: teslacamviewer
    environment:
      - PUID=1000
      - PGID=1000
      - TZ=America/Chicago
      - rootFolder=/teslacamdata 
      - authorizationEnabled=true #default is set to true
    volumes:
      - /home/rufogongora/teslamediashare:/teslacamdata #map your local tesla cam drive here
    ports:
      - 7544:80
    restart: unless-stopped
```

Once you run `docker-compose up` you should be able to access the site at `localhost:7544`

Please notice your root folder **must** follow the following structure:

- root folder -- this will be your local folder name
  - SavedClips
    - 2021-05-11_00-40-45 -- example folder
  - SentryClips

Also all clip folders **must** include the 4 camera angles .mp4 files and one `event.json` (now optional) file (these files are automatically generated by the tesla sentry system, so assuming you didn't manually delete any, you should be good)

For example:

- 2021-05-11_00-40-18-back.mp4
- 2021-05-11_00-40-18-front.mp4
- 2021-05-11_00-40-18-left_repeater.mp4
- 2021-05-11_00-40-18-right_repeater.mp4
- event.json (optional)
- thumb.png


# Known Issues

- Mp4 videos only work on firefox, chrome and edge may need an extra codec to work
- Deletion is disabled temporarily while I work on a soft deletion option
  

# Feature roadmap
- ✅ Add security login
- ✅ Clip Deletion Enabled -- Partially implemented, need to improve deletion times
- ✅ Save Favorites
- ❎ Natively work on chrome/edge
- ❎ Fancier look
- ❎ Merging of videos


Please send any feedback at @rufogongora