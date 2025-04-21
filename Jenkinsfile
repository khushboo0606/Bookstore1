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
                git 'https://github.com/khushboo0606/Bookstore1.git'
            }
        }

        stage('Restore Dependencies') {
            steps {
                bat 'dotnet restore Bookstore.sln'
            }
        }

        stage('Build Solution') {
            steps {
                bat 'dotnet build Bookstore.sln --no-restore'
            }
        }

        stage('Run Tests') {
            steps {
                bat 'dotnet test .\\Bookstore.Tests\\Bookstore.Tests.csproj --no-build --logger "trx;LogFileName=test_results.trx"'
            }
        }

        stage('Publish App') {
            steps {
                bat 'dotnet clean Bookstore\\Bookstore.csproj'
                bat "dotnet publish Bookstore\\Bookstore.csproj -c Release -o %PUBLISH_DIR%"
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
            cleanWs()
            echo 'Pipeline execution completed.'
        }
    }
}

