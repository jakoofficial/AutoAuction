use AutoAuction
GO

DROP PROCEDURE CreateAuction
GO

CREATE PROCEDURE CreateAuction 
@minimumPrice decimal(18,0),
@standingBid decimal(18,0), @vehicleId int, @seller varchar(max), @buyer varchar(max)
AS INSERT INTO dbo.Auction
(	minimumPrice, 
	standingBid,
	vehicleId,
	seller,
	buyer
)
VALUES (
	@minimumPrice,
	@standingBid, @vehicleId,
	@seller, @buyer
)

SELECT SCOPE