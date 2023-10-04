CREATE PROCEDURE [dbo].[GetAuction] @aID int
AS
SELECT * FROM Auction WHERE Auction.auctionId = @aID