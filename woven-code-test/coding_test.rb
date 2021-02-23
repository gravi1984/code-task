=begin
#coding_test

#sulotion:
Abstract of Robot walking environment into a x-y coordination;
Robot has recorded its states as (x coordinate, y coordinate, direction);
Robot excutes a list of commands received from input;
The alternation of Robot's states is effected by both command and last state;
The minimal distance Robot needs to get back is the sum of absolute x/y coordinate. 

#Robot moving model: 
------------> x
|
|
|
|
V y

# input example: F1,R1,B2,L1,B7`
# expected output: 8

=end 


#get terminal input of command string
print "input commands: "
input = gets

#parsed command string to array commands
commands = input.split(",")

#robot's state is spcified in [x coordinate, y coordinate, direction]
#robot's state is initilised as [0,0,0]
state = [0,0,0]

#function to exexute individual command in commands list
def execute_command(cmd, state)

#single command consists of: a command name in F/B/L/R, and a number(casted to integer)
	command = cmd[0]
	number = cmd[1].to_i

#switch command name, check current state, make moving decision: 
	case command

#forward command logic:  
	when "F"
# forward command logic: 
# check Robot's current direction stored in state;
# alter x or y coordinate according to current command and state 
	case state[2]
		when 0
			state[0]= state[0] + number
		when 90
			state[1]= state[1] + number
		when 180
			state[0]= state[0] - number
		when 270
			state[1]= state[1] - number
		else 
			puts "excepted direction"
		end 


# back command logic: 
	when "B"
# forward command logic: 
# check Robot's current direction stored in state;
# alter x or y coordinate according to current command and state 
		case state[2]
		when 0
			state[0]= state[0] - number
		when 90
			state[1]= state[1] - number
		when 180
			state[0]= state[0] + number
		when 270
			state[1]= state[1] + number
		else 
			puts "excepted direction"
		end 


# right command logic: 
# check Robot's current direction stored in state;
# alter direction in state according to current command and state 
	when "R"
#		puts "right 90*n"
		state[2] = (state[2] + 90*number)%360 
#		puts state[2]


# left command logic: 
# check Robot's current direction stored in state;
# alter direction in state according to current command and state 
	when "L"
#		puts "left 90*n"
		state[2] = (state[2] - 90*number)%360 
#		puts state[2]  

# 		puts state

	else
		puts "excepted command"  
end

end 

# execute all commands in received command list
commands.each do |cmd|
		execute_command cmd, state
end 

# output the minimum amount of distance Robot need to get back as the sum of absolute value of x and y:
puts "minimal distance is: #{state[0].abs + state[1].abs}"


