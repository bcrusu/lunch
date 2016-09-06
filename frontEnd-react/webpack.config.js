const path = require('path')
const webpack = require('webpack')

const isProd = process.env.NODE_ENV === 'production'
const isDev = !isProd

const config = {
  devtool: isDev ? "cheap-module-eval-source-map" : "hidden-source-map",
  entry: getEntry(),
  output: {
    path: path.join(__dirname, 'dist'),
    filename: 'lunch.js',
    publicPath: '/static/'
  },
  plugins: getPlugins(),
  module: {
    loaders: [
      { test: /\.js$/, loaders: ['babel'], exclude: /node_modules/, include: __dirname },
      { test: /\.css$/, loader: "style-loader!css-loader" },
      { test: /\.(woff|woff2)(\?v=\d+\.\d+\.\d+)?$/, loader: "url?limit=10000&mimetype=application/font-woff" },
      { test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/, loader: "url?limit=10000&mimetype=application/octet-stream" },
      { test: /\.eot(\?v=\d+\.\d+\.\d+)?$/, loader: "file" },
      { test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, loader: "url?limit=10000&mimetype=image/svg+xml" }
    ]
  },
  externals: {
    "react": "React",
    "react-dom": "ReactDOM"
  }
}

function getEntry() {
  let result = []
  result.push('./index')

  if (isDev)
    result.push('webpack-hot-middleware/client')

  return result
}

function getPlugins() {
  let result = [] 

  if (isProd) {
    result.push(new webpack.optimize.DedupePlugin())
    result.push(new webpack.optimize.OccurrenceOrderPlugin())
    result.push(new webpack.optimize.UglifyJsPlugin({
      sourceMap: false,
      compress: {
        warnings: false
      },
      output: {
        comments: false,
        beautify: false
      }
    }))
  }
  else {
    result.push(new webpack.HotModuleReplacementPlugin())
  }

  return result
}

module.exports = config
