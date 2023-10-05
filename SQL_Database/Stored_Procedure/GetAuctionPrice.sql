CREATE PROCEDURE GetAuctionPrice
    @auctionId INT
AS
BEGIN
    DECLARE @price DECIMAL(18, 2) = 0;

    SELECT @price = CASE 
                        WHEN minimumPrice >= standingBid THEN minimumPrice
                        ELSE standingBid
                    END
    FROM Auction
    WHERE auctionId = @auctionId;

    IF @price IS NULL
    BEGIN
        SET @price = 0; 
    END

    SELECT @price AS AuctionPrice;
END;