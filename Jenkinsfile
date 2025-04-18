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

        stage('Build') {
            steps {
                bat 'dotnet build'
            }
        }

        stage('Test') {
    steps {
        bat 'dotnet test Bookstore.Tests/Bookstore.Tests.csproj --logger "trx;LogFileName=test_results.trx"'
        junit allowEmptyResults: true, testResults: '**/TestResults/*.trx'
    }
}

        stage('Publish') {
            steps {
                bat 'dotnet clean'
                bat 'dotnet publish -c Release -o publish_output'
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
