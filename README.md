# Automatic internet speed tests

This .Net 4.7 console application runs a speedtest using [speedtest-cli](https://github.com/sivel/speedtest-cli) at specified intervals, logging the results to a database.

This project is licensed under the Apache 2.0 license.  See License.md.

# Dependencies
* SqlServer localdb installed locally (otherwise you can modify the connection string in App.config)
* Python
* [speedtest-cli](https://github.com/sivel/speedtest-cli)

# Setup
In App.config, under appSettings, update the path to speedtest-cli as appropriate