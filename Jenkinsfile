pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        IMAGE_NAME = 'bookstorewebapp'
        CONTAINER_NAME = 'bookstore-container'
        PUBLISH_DIR = 'publish_output'
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
                bat 'dotnet test .\\Bookstore.Tests\\Bookstore.Tests.csproj --no-build --logger "trx;LogFileName=test_results.trx"'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet clean'
                bat "dotnet publish .\\Bookstore.csproj -c Release -o %PUBLISH_DIR%"
            }
        }

        stage('Docker Build') {
            steps {
                bat "docker build -t %IMAGE_NAME% ."
            }
        }

        stage('Docker Run') {
            steps {
                bat '''
                    docker stop %CONTAINER_NAME% || echo Container not running
                    docker rm %CONTAINER_NAME% || echo Container not found
                    docker run -d -p 5000:80 --name %CONTAINER_NAME% %IMAGE_NAME%
                '''
            }
        }
    }

    post {
        always {
            echo 'Pipeline execution completed.'
        }
    }
}
