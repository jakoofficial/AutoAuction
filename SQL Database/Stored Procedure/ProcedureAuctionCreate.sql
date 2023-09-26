use AutoAuction
GO

CREATE PROCEDURE CreateAuction 
@auctionNumber int, @minimumPrice decimal(18,0),
@standingBid decimal(18,0), @vehicleId int, @seller varchar(max), @buyer varchar(max)
AS INSERT INTO dbo.Auction
(	auctionNumber, 
	minimumPrice, 
	standingBid,
	vehicleId,
	seller,
	buyer
)
VALUES (
	@auctionNumber, @minimumPrice,
	@standingBid, @vehicleId,
	@seller, @buyer
)