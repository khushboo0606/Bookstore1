pipeline {
    agent any

    environment {
        DOCKER_IMAGE = "bookstore1-image"
    }

    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/khushboo0606/Bookstore1.git'
            }
        }

        stage('Restore Dependencies') {
            steps {
                script {
                    // Restoring dependencies before build
                    bat 'dotnet restore Bookstore.sln'
                }
            }
        }

        stage('Build Solution') {
            steps {
                script {
                    // Build the project
                    bat 'dotnet build Bookstore.sln --no-restore'
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    // Build Docker image
                    docker.build(DOCKER_IMAGE)
                }
            }
        }

        stage('Run Docker Container') {
            steps {
                script {
                    // Run the Docker container
                    docker.image(DOCKER_IMAGE).run('-d -p 8080:80')
                }
            }
        }
    }

    post {
        always {
            cleanWs() // Clean workspace after build
        }
    }
}