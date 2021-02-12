# Instagram GAN generator

## About

These projects are designed for handling remote image urls. Typically these are passed via RabbitMQ by the InstagramDownloader project. Urls are retrieved by the ImageFilter service, which validates the image against the Google cloud vision api. If the image is validated, this is then passed to the ImageDownloader service for downloading, resizing, and saving.