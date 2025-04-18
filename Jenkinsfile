pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore Bookstore.sln'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build Bookstore.sln --no-restore'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test ./Bookstore.Tests/Bookstore.Tests.csproj --no-build --logger "trx;LogFileName=test_results.trx"'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet clean'
                bat 'dotnet publish ./Bookstore.csproj -c Release -o publish_output'
            }
        }

        stage('Docker Build') {
            steps {
                bat 'docker build -t bookstorewebapp .'
            }
        }

        stage('Docker Run') {
            steps {
                bat '''
                    docker stop bookstore-container || echo "Container not running"
                    docker rm bookstore-container || echo "Container not found"
                    docker run -d -p 5140:80 --name bookstore-container bookstorewebapp
                '''
            }
        }
    }
}
