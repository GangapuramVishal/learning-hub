# filename: ytd_gain_tech_companies.py
import yfinance as yf
from datetime import datetime

def get_ytd_gain(ticker):
    try:
        # Get stock data
        stock = yf.Ticker(ticker)
        history = stock.history(period="ytd")
        
        # Extract the first and last closing prices of the year
        start_price = history['Close'].iloc[0]
        end_price = history['Close'].iloc[-1]
        
        # Calculate YTD gain
        ytd_gain = ((end_price - start_price) / start_price) * 100
        return round(ytd_gain, 2)
    except Exception as e:
        return f"Error fetching data for {ticker}: {e}"

# List of 10 largest technology companies by ticker symbol
tech_companies = [
    "AAPL",  # Apple
    "MSFT",  # Microsoft
    "GOOGL", # Alphabet (Google)
    "AMZN",  # Amazon
    "NVDA",  # NVIDIA
    "TSLA",  # Tesla
    "META",  # Meta Platforms (Facebook)
    "ORCL",  # Oracle
    "ADBE",  # Adobe
    "CRM"    # Salesforce
]

# Fetch and display YTD gain for each company
print("YTD Gain of 10 Largest Technology Companies:")
for company in tech_companies:
    ytd_gain = get_ytd_gain(company)
    print(f"{company}: {ytd_gain}%")