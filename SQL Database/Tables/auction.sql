USE AutoAuction

DROP TABLE IF EXISTS dbo.Auction
GO

CREATE TABLE dbo.Auction(
	auctionId int IDENTITY(1,1) Primary key,
	minimumPrice decimal(18,0) not null,
	standingBid decimal(18,0) not null,
	vehicleId int not null,
	seller varchar(max) not null,
	buyer varchar(max),
);