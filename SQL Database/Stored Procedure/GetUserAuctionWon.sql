CREATE PROCEDURE GetUserAuctionWon @buyer varchar(300)
AS
BEGIN
DECLARE @AuctionsWon INT;
    
    SELECT @AuctionsWon = COUNT(*) 
    FROM dbo.Auction 
    WHERE buyer = @buyer AND active = 0;
    

    SELECT @AuctionsWon AS AuctionsWon;
END;