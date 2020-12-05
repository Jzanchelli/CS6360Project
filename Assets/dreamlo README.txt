LeaderBoardLink:http://dreamlo.com/lb/7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A
PrivateCode:7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A
PublicCode:5fca6169eb36fd27143b8b60
WebsiteEmbedCode:<style type="text/css">
.dreamloLBTable { border-collapse:collapse;text-align:center;width: 200px; }
.dreamloLBTable th { border-bottom: 1px solid #EEEEEE;font-weight:bold;margin:0;padding:4px; }
.dreamloLBTable td { border-bottom: 1px solid#EEEEEE;margin:0;padding:4px; }
</style>

<script src="http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/js" type="text/javascript"></script>

Examples:
	Adding and deleting scores
	Changes and updates to your leaderboard are made through simple http get requests using your private url.
		
	A player named Carmine got a score of 100. If the same name is added twice, we use the higher score.
		http://dreamlo.com/lb/7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A/add/Carmine/100
	
	A player named Carmine got a score of 1000 in 90 seconds.
		http://dreamlo.com/lb/7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A/add/Carmine/1000/90
	
	A player named Carmine got a score of 1000 in 90 seconds and is Awesome.
		http://dreamlo.com/lb/7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A/add/Carmine/1000/90/Awesome
	
	Delete Carmine's score
		http://dreamlo.com/lb/7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A/delete/Carmine
	
	Clear all scores
		http://dreamlo.com/lb/7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A/clear
	
	Save a trip to the server by combining "add" with returning data: "add-pipe", "add-xml" or "add-quote"
		http://dreamlo.com/lb/7Fz75i73mU-DG2k3Sksw5geKQTBM_1qkCYPxBHklv_-A/add-pipe/Carmine/100
		
	Getting your scores
	Reading of data is performed by using your public url.
	Get your data as XML:
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/xml
	
	Get your data as json:
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/json
	
	Get your data as pipe delimited:
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/pipe
	
	Get your data as quoted with comma:
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/quote
	
	To get the list sorted by seconds (high to low) just add -seconds:
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/xml-seconds
	
	To sort ascending just add -asc:
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/xml-seconds-asc
	
	To get the list sorted by date (newest to oldest) just add -date:
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/xml-date
	
	To get only the top 5 or 10 rows, just add the number to the end:
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/xml/5
	
	To get a range of rows, use a starting number and count of rows you want
	For instance, this is rows 100 to 149 (starts with number 0) :
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/xml/100/50
	
	
	To get the score for just 1 person, just add -get and the person's name:
		http://dreamlo.com/lb/5fca6169eb36fd27143b8b60/pipe-get/Carmine
	
	