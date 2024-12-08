# Sympli SEO Search Project

## Overview
This project implements an application to assist the CEO of Sympli in automatically identifying where their company’s URL (`www.sympli.com.au`) ranks in search results for specified keywords (e.g., `"e-settlements"`). The application retrieves the positions of the URL in the first 100 results of a search engine and outputs the results.

## Features
1. **Google Search Simulation**:
   - Finds and returns the positions of the URL for the given keywords within the first 100 results.
   - If the URL is not found, it returns `0`.

2. **Caching** (Extension 1):
   - Limits search calls to once per hour for the same keyword and URL combination by using an in-memory cache.

3. **Multiple Search Engines Support** (Extension 2):
   - Supports multiple search engines (e.g., Google, Bing) with a flexible architecture for future extensions.

## Techstack:
   - Back-end: ASP.NET Core 8, CQRS pattern, Factory Method pattern, Vertical Slide Architecture, DI (MediatR & FluentValidation), Caching, Unit Test (XUnit)
   - Front-end: ReactJS, Ant, TypeScript
---