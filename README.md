This assesment repo fulfils following requirements with unit tests:

1. Develop a web page on which users will input 

    - Date the book is checked out.
    - Date the book is returned.
    - Country selection (at least 2 different countries)
    - Calculate button

2. The web page should display
    - Calculated Business Days
    - Calculated penalty

3. Penalty should be calculated for BUSINESS days only between the checkout and returned days of the book. The checkout and return dates are inclusive.
    - It should consider weekends and national holidays/religious holidays defined in tables in database for a specific country.
    - Note that some countries have different working days and weekends. For example in Dubai Friday and Saturday are off days. However in Turkey Saturday and Sunday are off days.
    - Your code should not have these assumptions hardcoded but they must be in configurations. The configuration should be kept in a database table.
    - Do not provide a screen to edit these values in the database. Manual editing of these values is sufficient.
    - You should develop your own algorithm for business day count.

4. A book should be returned in 10 days. Any business day after 10 days will be considered as a late day.

    - Each late business day will be penalized for 5.00 $ (or currency code of country)
    - The currency code and the amount is country specific.
    - Penalty amount should be a decimal value to accommodate for cents and fills and etc.

5. Any monetary value you display on the screen should have proper formatting
